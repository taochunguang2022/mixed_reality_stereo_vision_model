
import numpy as np
import torch


def random_split(n=12000, train_ratio=0.8, val_ratio=0.1, test_ratio=0.1):
    """Use 9:1:1 split"""
    p_train = train_ratio
    p_val = val_ratio
    p_test = test_ratio

    n = 12000  # original 12000 patients, remove 12 outliers
    n_train = round(n * p_train)
    n_val = round(n * p_val)
    n_test = n - (n_train + n_val)
    p = np.random.permutation(n)
    idx_train = p[:n_train]
    idx_val = p[n_train:n_train + n_val]
    idx_test = p[n_train + n_val:]
    return idx_train, idx_val, idx_test


def get_data_split(base_path, split_path, split_type='random', reverse=False, baseline=True, dataset='P36', predictive_label='fatigue'):
    # load data
    if dataset == 'P12':
        Pdict_list = np.load(base_path + '/processed_data/PTdict_list.npy', allow_pickle=True)
        arr_outcomes = np.load(base_path + '/processed_data/arr_outcomes.npy', allow_pickle=True)
        dataset_prefix = ''
    elif dataset == 'P36':
        Pdict_list = np.load(base_path + '/processed_data/PTdict_list.npy', allow_pickle=True)
        arr_outcomes = np.load(base_path + '/processed_data/arr_outcomes.npy', allow_pickle=True)
        dataset_prefix = 'P36_'


    if baseline==True:
        BL_path = ''
    else:
        BL_path = 'ohtermodels/'

    if split_type == 'random':
        # load random indices from a split
        idx_train, idx_val, idx_test = np.load(base_path + split_path, allow_pickle=True)

    elif split_type == 'gender':
        if reverse == False:
            idx_train = np.load(BL_path+'saved/' + dataset_prefix + 'idx_male.npy', allow_pickle=True)
            idx_vt = np.load(BL_path+'saved/' + dataset_prefix + 'idx_female.npy', allow_pickle=True)
        elif reverse == True:
            idx_train = np.load(BL_path+'saved/' + dataset_prefix + 'idx_female.npy', allow_pickle=True)
            idx_vt = np.load(BL_path+'saved/' + dataset_prefix + 'idx_male.npy', allow_pickle=True)

        np.random.shuffle(idx_vt)
        idx_val = idx_vt[:round(len(idx_vt) / 2)]
        idx_test = idx_vt[round(len(idx_vt) / 2):]

    # extract train/val/test examples

    Ptrain = Pdict_list[idx_train]
    Pval = Pdict_list[idx_val]
    Ptest = Pdict_list[idx_test]

    if dataset == 'P12' or dataset == 'P36':
        if predictive_label == 'fatigue':
            y = arr_outcomes[:, -1].reshape((-1, 1))

    ytrain = y[idx_train]
    yval = y[idx_val]
    ytest = y[idx_test]

    return Ptrain, Pval, Ptest, ytrain, yval, ytest


def getStats(P_tensor: object) -> object:
    N, T, F = P_tensor.shape
    Pf = P_tensor.transpose((2, 0, 1)).reshape(F, -1)
    mf = np.zeros((F, 1))
    stdf = np.ones((F, 1))
    eps = 1e-7
    for f in range(F):
        vals_f = Pf[f, :]
        vals_f = vals_f[vals_f > 0]#有值的
        mf[f] = np.mean(vals_f)
        stdf[f] = np.std(vals_f)
        stdf[f] = np.max([stdf[f], eps])
    return mf, stdf


def mask_normalize(P_tensor, mf, stdf):
    """ Normalize time series variables. Missing ones are set to zero after normalization. """
    N, T, F = P_tensor.shape
    Pf = P_tensor.transpose((2, 0, 1)).reshape(F, -1)
    M = 1*(P_tensor > 0) + 0*(P_tensor <= 0)   # 做了个标志位   1：有值 0：没值
    M_3D = M.transpose((2, 0, 1)).reshape(F, -1)
    for f in range(F):
        Pf[f] = (Pf[f]-mf[f])/(stdf[f]+1e-18)
    Pf = Pf * M_3D#就是让有值的地方才能标准化完是有值的，之前没值的地方现在还是0
    Pnorm_tensor = Pf.reshape((F, N, T)).transpose((1, 2, 0))
    Pfinal_tensor = np.concatenate([Pnorm_tensor, M], axis=2)#做了个拼接 M相当于标识符了
    return Pfinal_tensor


def getStats_static(P_tensor, dataset='P36'):
    N, S = P_tensor.shape
    Ps = P_tensor.transpose((1, 0))
    ms = np.zeros((S, 1))
    ss = np.ones((S, 1))

    if dataset == 'P12':

        bool_categorical = [1, 1]

    elif dataset == 'P36':

        bool_categorical = [1, 1]

    for s in range(S):
        if bool_categorical == 0:  # if not categorical
            vals_s = Ps[s, :]
            vals_s = vals_s[vals_s > 0]
            ms[s] = np.mean(vals_s)
            ss[s] = np.std(vals_s)
    return ms, ss


def mask_normalize_static(P_tensor, ms, ss):
    N, S = P_tensor.shape
    Ps = P_tensor.transpose((1, 0))

    # input normalization
    for s in range(S):
        Ps[s] = (Ps[s] - ms[s]) / (ss[s] + 1e-18)

    # set missing values to zero after normalization
    for s in range(S):
        idx_missing = np.where(Ps[s, :] <= 0)
        Ps[s, idx_missing] = 0

    # reshape back
    Pnorm_tensor = Ps.reshape((S, N)).transpose((1, 0))
    return Pnorm_tensor


def tensorize_normalize(P, y, mf, stdf, ms, ss):
    T, F = P[0]['arr'].shape
    D = len(P[0]['extended_static'])

    P_tensor = np.zeros((len(P), T, F))
    P_time = np.zeros((len(P), T, 1))
    P_static_tensor = np.zeros((len(P), D))
    for i in range(len(P)):
        P_tensor[i] = P[i]['arr']
        P_time[i] = P[i]['time']
        P_static_tensor[i] = P[i]['extended_static']
    P_tensor = mask_normalize(P_tensor, mf, stdf)
    P_tensor = torch.Tensor(P_tensor)

    P_time = torch.Tensor(P_time) / 60.0  # convert mins to hours
    P_static_tensor = mask_normalize_static(P_static_tensor, ms, ss)
    P_static_tensor = torch.Tensor(P_static_tensor)

    y_tensor = y
    y_tensor = torch.Tensor(y_tensor[:, 0]).type(torch.LongTensor)
    return P_tensor, P_static_tensor, P_time, y_tensor


def tensorize_normalize_other(P, y, mf, stdf):
    T, F = P[0].shape
    P_time = np.zeros((len(P), T, 1))
    for i in range(len(P)):
        tim = torch.linspace(0, T, T).reshape(-1, 1)
        P_time[i] = tim
    P_tensor = mask_normalize(P, mf, stdf)
    P_tensor = torch.Tensor(P_tensor)

    P_time = torch.Tensor(P_time) / 60.0

    y_tensor = y
    y_tensor = torch.Tensor(y_tensor[:, 0]).type(torch.LongTensor)
    return P_tensor, None, P_time, y_tensor


def masked_softmax(A, epsilon=0.000000001):
    A_max = torch.max(A, dim=1, keepdim=True)[0]
    A_exp = torch.exp(A - A_max)
    A_exp = A_exp * (A != 0).float()
    A_softmax = A_exp / (torch.sum(A_exp, dim=0, keepdim=True) + epsilon)
    return A_softmax


def random_sample(idx_0, idx_1, B, replace=False):
    """ Returns a balanced sample of tensors by randomly sampling without replacement. """
    idx0_batch = np.random.choice(idx_0, size=int(B / 2), replace=replace)
    idx1_batch = np.random.choice(idx_1, size=int(B / 2), replace=replace)
    idx = np.concatenate([idx0_batch, idx1_batch], axis=0)
    return idx


def evaluate(model, P_tensor, P_time_tensor, P_static_tensor, batch_size=100, n_classes=2, static=1):
    model.eval()
    P_tensor = P_tensor.cuda()
    P_time_tensor = P_time_tensor.cuda()
    if static is None:
        Pstatic = None
    else:
        P_static_tensor = P_static_tensor.cuda()
        N, Fs = P_static_tensor.shape

    T, N, Ff = P_tensor.shape
    n_batches, rem = N // batch_size, N % batch_size
    out = torch.zeros(N, n_classes)
    start = 0
    for i in range(n_batches):
        P = P_tensor[:, start:start + batch_size, :]
        Ptime = P_time_tensor[:, start:start + batch_size]
        if P_static_tensor is not None:
            Pstatic = P_static_tensor[start:start + batch_size]
        lengths = torch.sum(Ptime > 0, dim=0)
        middleoutput, _, _ = model.forward(P, Pstatic, Ptime, lengths)
        out[start:start + batch_size] = middleoutput.detach().cpu()
        start += batch_size
    if rem > 0:
        P = P_tensor[:, start:start + rem, :]
        Ptime = P_time_tensor[:, start:start + rem]
        if P_static_tensor is not None:
            Pstatic = P_static_tensor[start:start + batch_size]
        lengths = torch.sum(Ptime > 0, dim=0)
        whatever, _, _ = model.forward(P, Pstatic, Ptime, lengths)
        out[start:start + rem] = whatever.detach().cpu()
    return out


def evaluate_standard(model, P_tensor, P_time_tensor, P_static_tensor, batch_size=100, n_classes=2, static=1):
    P_tensor = P_tensor.cuda()
    P_time_tensor = P_time_tensor.cuda()
    if static is None:
        P_static_tensor = None
    else:
        P_static_tensor = P_static_tensor.cuda()

    lengths = torch.sum(P_time_tensor > 0, dim=0)
    out, _, _ = model.forward(P_tensor, P_static_tensor, P_time_tensor, lengths)
    return out

