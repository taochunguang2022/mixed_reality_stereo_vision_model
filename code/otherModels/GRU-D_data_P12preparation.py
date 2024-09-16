import numpy as np
import pandas as pd
import os
import math
import matplotlib.pyplot as plt


# functions to process the time in the data
def timeparser(time):
    return pd.to_timedelta(time + ':00')


def timedelta_to_day_figure(timedelta):
    return timedelta.days + (timedelta.seconds/86400)


def df_to_inputs(df, inputdict, inputs):    # group the data by time
    grouped_data = df.groupby('Time')
    for row_index, value in df.iterrows():
        if isinstance(value.Parameter, str) or (isinstance(value.Parameter, float) and not math.isnan(value.Parameter)):
            agg_no = inputdict[value.Parameter]
            inputs[agg_no].append(value.Value)
    return inputs


def describe(inputs, input_columns, inputdict, hist=False):
    desc = []

    for i in range(len(inputdict)):
        input_arr = np.asarray(inputs[i])

        des = []
        des.append(input_arr.size)
        des.append(np.amin(input_arr))
        des.append(np.amax(input_arr))
        des.append(np.mean(input_arr))
        des.append(np.median(input_arr))
        des.append(np.std(input_arr))
        des.append(np.var(input_arr))

        desc.append(des)

        if hist:
            a = np.hstack(input_arr)
            plt.hist(a, bins='auto')
            plt.title("Histogram about {}".format(input_columns[i]))
            plt.show()

        print('count: {}, min: {}, max: {}'.format(des[0], des[1], des[2]))
        print('mean: {}, median: {}, std: {}, var: {}'.format(des[3], des[4], des[5], des[6]))

    return desc


def df_to_x_m_d(df, inputdict, size, id_posistion, split, dataset_name='P12'):
    grouped_data = df.groupby('Time')

    #generate input vectors
    if dataset_name == 'P12' or dataset_name == 'P36':
        x = np.zeros((len(inputdict), grouped_data.ngroups))
        masking = np.zeros((len(inputdict), grouped_data.ngroups))

    delta = np.zeros((split, size))
    if dataset_name == 'PAM':
        timetable = np.zeros(600)
    else:
        timetable = np.zeros(grouped_data.ngroups)
    id = 0

    all_x = np.zeros((split,1))

    s_dataset = np.zeros((12, split, size))

    if grouped_data.ngroups > size:
        # fill the x and masking vectors
        if dataset_name == 'P12' or dataset_name == 'P36':
            pre_time = pd.to_timedelta(0)


        t = 0
        for row_index, value in df.iterrows():
            if isinstance(value.Parameter, str) or (isinstance(value.Parameter, float) and not math.isnan(value.Parameter)):
                agg_no = inputdict[value.Parameter]

            # same timeline check.
            if pre_time != value.Time:
                pre_time = value.Time
                t += 1
                if dataset_name == 'P12' or dataset_name == 'P36':
                    timetable[t] = timedelta_to_day_figure(value.Time)


            x[agg_no, t] = value.Value
            masking[agg_no, t] = 1

        # generate index that has most parameters and first/last one.
        ran_index = grouped_data.count()
        ran_index = ran_index.reset_index()
        ran_index = ran_index.sort_values('Value', ascending=False)
        ran_index = ran_index[:size]
        ran_index = ran_index.sort_index()
        ran_index = np.asarray(ran_index.index.values)
        ran_index[0] = 0
        ran_index[size-1] = grouped_data.ngroups-1

        # take id for outcome comparing
        id = x[id_posistion, 0]

        x = x[:split, :]
        masking = masking[:split, :]

        x_sample = np.zeros((split, size))
        m_sample = np.zeros((split, size))
        time_sample = np.zeros(size)

        t_x_sample = x_sample.T
        t_marsking = m_sample.T

        t_x = x.T
        t_m = masking.T

        it = np.nditer(ran_index, flags=['f_index'])
        while not it.finished:
            t_x_sample[it.index] = t_x[it[0]]
            t_marsking[it.index] = t_m[it[0]]
            time_sample[it.index] = timetable[it[0]]
            it.iternext()

        x = x_sample
        masking = m_sample
        timetable = time_sample

        # fill the delta vectors
        for index, value in np.ndenumerate(masking):
            if index[1] == 0:
                delta[index[0], index[1]] = 0
            elif masking[index[0], index[1]-1] == 0:
                delta[index[0], index[1]] = timetable[index[1]] - timetable[index[1]-1] + delta[index[0], index[1]-1]
            else:
                delta[index[0], index[1]] = timetable[index[1]] - timetable[index[1]-1]

    else:
        # fill the x and masking vectors
        if dataset_name == 'P12' or dataset_name == 'P36':
            pre_time = pd.to_timedelta(0)


        t = 0
        for row_index, value in df.iterrows():
            if isinstance(value.Parameter, str) or (isinstance(value.Parameter, float) and not math.isnan(value.Parameter)):
                agg_no = inputdict[value.Parameter]

            # same timeline check.
            if pre_time != value.Time:
                pre_time = value.Time
                t += 1
                if dataset_name == 'P12' or dataset_name == 'P36':
                    timetable[t] = timedelta_to_day_figure(value.Time)

            x[agg_no, t] = value.Value
            masking[agg_no, t] = 1

        # take id for outcome comparing
        id = x[id_posistion, 0]

        x = x[:split, :]
        masking = masking[:split, :]

        x = np.pad(x, ((0,0), (size-grouped_data.ngroups, 0)), 'constant')
        masking = np.pad(masking, ((0,0), (size-grouped_data.ngroups, 0)), 'constant')
        timetable = np.pad(timetable, (size-grouped_data.ngroups, 0), 'constant')

        # fill the delta vectors
        for index, value in np.ndenumerate(masking):
            if index[1] == 0:
                delta[index[0], index[1]] = 0
            elif masking[index[0], index[1]-1] == 0:
                delta[index[0], index[1]] = timetable[index[1]] - timetable[index[1]-1] + delta[index[0], index[1]-1]
            else:
                delta[index[0], index[1]] = timetable[index[1]] - timetable[index[1]-1]

    all_x = np.concatenate((all_x, x), axis=1)
    all_x = all_x[:, 1:]

    s_dataset[0] = x
    s_dataset[1] = masking
    s_dataset[2] = delta

    return s_dataset, all_x, id


def get_mean(x):
    x_mean = []
    for i in range(x.shape[0]):
        mean = np.mean(x[i])
        x_mean.append(mean)
    return x_mean


def get_median(x):
    x_median = []
    for i in range(x.shape[0]):
        median = np.median(x[i])
        x_median.append(median)
    return x_median


def get_std(x):
    x_std = []
    for i in range(x.shape[0]):
        std = np.std(x[i])
        x_std.append(std)
    return x_std


def get_var(x):
    x_var = []
    for i in range(x.shape[0]):
        var = np.var(x[i])
        x_var.append(var)
    return x_var


def dataset_normalize(dataset, mean, std):
    for i in range(dataset.shape[0]):
        dataset[i][0] = (dataset[i][0] - mean[:, None])
        dataset[i][0] = dataset[i][0]/std[:, None]
    return dataset


def normalize_chk(dataset):
    all_x_add = np.zeros((dataset[0][0].shape[0],1))
    for i in range(dataset.shape[0]):
        all_x_add = np.concatenate((all_x_add, dataset[i][0]), axis=1)

    mean = get_mean(all_x_add)
    median = get_median(all_x_add)
    std = get_std(all_x_add)
    var = get_var(all_x_add)

    return mean, median, std, var


def df_to_y1(df):
    output = df.values
    output = output[:, 1:]  # for fatigue


    return output


if __name__ == '__main__':
    dataset_name = 'P12'  # possible values: 'P12', 'P36'
    print('Dataset used: ', dataset_name)

    if dataset_name == 'P12':
        inputpath_1 = '../../P12data/rawdata/djy/'
        inputpath_2 = '../../P12data/rawdata/gxx/'
        inputpath_3 = '../../P12data/rawdata/jsw/'
        inputpath_4 = '../../P12data/rawdata/jxt/'
        inputpath_5 = '../../P12data/rawdata/lfz/'
        inputpath_6 = '../../P12data/rawdata/lh/'
        inputpath_7 = '../../P12data/rawdata/mtq/'
        inputpath_8 = '../../P12data/rawdata/qc/'
        inputpath_9 = '../../P12data/rawdata/qjw/'
        inputpath_10 = '../../P12data/rawdata/zdf/'
        inputpath_11 = '../../P12data/rawdata/zhs/'
        inputpath_12 = '../../P12data/rawdata/zwl/'
        inputpath_13 = '../../P12data/rawdata/zzt/'

        inputdict = {
            "chestHR": 0,  # o
            "chestIBI": 1,  # o
            "chestRPeak": 2,  # o
            "chestRI": 3,  # o
            "chestRPeaks": 4,  # o
            "chestRR": 5,  # o
            "fingerEDA": 6,  # o
            "fingerEDASC": 7,  # o
            "fingerEDATD": 8,  # o
            "fingerEDAPD": 9,  # o
            "fingerSpO2": 10,
            "fingerSKT": 11,  # o
            "RecordID": 12,  # unused variable
            "Gender": 13,  # unused variable
        }

        inputs = []

        # prepare empty list to put data
        for i in range(len(inputdict)):
            t = []
            inputs.append(t)

        # read all the files in the input folder
        for filename in os.listdir(inputpath_1):
            df = pd.read_csv(inputpath_1 + filename, header=0, parse_dates=['Time'], date_parser=timeparser)

            inputs = df_to_inputs(df=df, inputdict=inputdict, inputs=inputs)

        for filename in os.listdir(inputpath_2):
            df = pd.read_csv(inputpath_2 + filename, header=0, parse_dates=['Time'], date_parser=timeparser)

            inputs = df_to_inputs(df=df, inputdict=inputdict, inputs=inputs)

        for filename in os.listdir(inputpath_3):
            df = pd.read_csv(inputpath_3 + filename, header=0, parse_dates=['Time'], date_parser=timeparser)

            inputs = df_to_inputs(df=df, inputdict=inputdict, inputs=inputs)

        for filename in os.listdir(inputpath_4):
            df = pd.read_csv(inputpath_4 + filename, header=0, parse_dates=['Time'], date_parser=timeparser)

            inputs = df_to_inputs(df=df, inputdict=inputdict, inputs=inputs)

        for filename in os.listdir(inputpath_5):
            df = pd.read_csv(inputpath_5 + filename, header=0, parse_dates=['Time'], date_parser=timeparser)

            inputs = df_to_inputs(df=df, inputdict=inputdict, inputs=inputs)

        for filename in os.listdir(inputpath_6):
            df = pd.read_csv(inputpath_6 + filename, header=0, parse_dates=['Time'], date_parser=timeparser)

            inputs = df_to_inputs(df=df, inputdict=inputdict, inputs=inputs)

        for filename in os.listdir(inputpath_7):
            df = pd.read_csv(inputpath_7 + filename, header=0, parse_dates=['Time'], date_parser=timeparser)

            inputs = df_to_inputs(df=df, inputdict=inputdict, inputs=inputs)

        for filename in os.listdir(inputpath_8):
            df = pd.read_csv(inputpath_8 + filename, header=0, parse_dates=['Time'], date_parser=timeparser)

            inputs = df_to_inputs(df=df, inputdict=inputdict, inputs=inputs)

        for filename in os.listdir(inputpath_9):
            df = pd.read_csv(inputpath_9 + filename, header=0, parse_dates=['Time'], date_parser=timeparser)

            inputs = df_to_inputs(df=df, inputdict=inputdict, inputs=inputs)

        for filename in os.listdir(inputpath_10):
            df = pd.read_csv(inputpath_10 + filename, header=0, parse_dates=['Time'], date_parser=timeparser)

            inputs = df_to_inputs(df=df, inputdict=inputdict, inputs=inputs)

        for filename in os.listdir(inputpath_11):
            df = pd.read_csv(inputpath_11 + filename, header=0, parse_dates=['Time'], date_parser=timeparser)

            inputs = df_to_inputs(df=df, inputdict=inputdict, inputs=inputs)

        for filename in os.listdir(inputpath_12):
            df = pd.read_csv(inputpath_12 + filename, header=0, parse_dates=['Time'], date_parser=timeparser)

            inputs = df_to_inputs(df=df, inputdict=inputdict, inputs=inputs)

        for filename in os.listdir(inputpath_13):
            df = pd.read_csv(inputpath_13 + filename, header=0, parse_dates=['Time'], date_parser=timeparser)

            inputs = df_to_inputs(df=df, inputdict=inputdict, inputs=inputs)


        # save inputs just in case
        np.save('P12saved/inputs.npy', inputs, allow_pickle=True)
        loaded_inputs = np.load('P12saved/inputs.npy', allow_pickle=True)

        # make input items list
        input_columns = list(inputdict.keys())

        desc = describe(loaded_inputs, input_columns, inputdict, hist=False)
        desc = np.asarray(desc)
        print(desc.shape)

        # 0: count, 1: min, 2: max, 3: mean, 4: median, 5: std, 6: var
        np.save('P12saved/desc.npy', desc)
        loaded_desc = np.load('P12saved/desc.npy')

        size = 49  # steps ~ from the paper
        id_posistion = 12
        input_length = 12 # input variables ~ from the paper
        dataset = np.zeros((1, 12, input_length, size))

        all_x_add = np.zeros((input_length, 1))

        for filename in os.listdir(inputpath_1):
            df = pd.read_csv(inputpath_1 + filename, header=0, parse_dates=['Time'], date_parser=timeparser)
            s_dataset, all_x, id = df_to_x_m_d(df=df, inputdict=inputdict, size=size, id_posistion=id_posistion, split=input_length)

            dataset = np.concatenate((dataset, s_dataset[np.newaxis, :, :, :]))
            all_x_add = np.concatenate((all_x_add, all_x), axis=1)

        for filename in os.listdir(inputpath_2):
            df = pd.read_csv(inputpath_2 + filename, header=0, parse_dates=['Time'], date_parser=timeparser)
            s_dataset, all_x, id = df_to_x_m_d(df=df, inputdict=inputdict, size=size, id_posistion=id_posistion,
                                               split=input_length)

            dataset = np.concatenate((dataset, s_dataset[np.newaxis, :, :, :]))
            all_x_add = np.concatenate((all_x_add, all_x), axis=1)

        for filename in os.listdir(inputpath_3):
            df = pd.read_csv(inputpath_3 + filename, header=0, parse_dates=['Time'], date_parser=timeparser)
            s_dataset, all_x, id = df_to_x_m_d(df=df, inputdict=inputdict, size=size, id_posistion=id_posistion,
                                               split=input_length)

            dataset = np.concatenate((dataset, s_dataset[np.newaxis, :, :, :]))
            all_x_add = np.concatenate((all_x_add, all_x), axis=1)

        for filename in os.listdir(inputpath_4):
            df = pd.read_csv(inputpath_4 + filename, header=0, parse_dates=['Time'], date_parser=timeparser)
            s_dataset, all_x, id = df_to_x_m_d(df=df, inputdict=inputdict, size=size, id_posistion=id_posistion, split=input_length)

            dataset = np.concatenate((dataset, s_dataset[np.newaxis, :, :, :]))
            all_x_add = np.concatenate((all_x_add, all_x), axis=1)

        for filename in os.listdir(inputpath_5):
            df = pd.read_csv(inputpath_5 + filename, header=0, parse_dates=['Time'], date_parser=timeparser)
            s_dataset, all_x, id = df_to_x_m_d(df=df, inputdict=inputdict, size=size, id_posistion=id_posistion,
                                               split=input_length)

            dataset = np.concatenate((dataset, s_dataset[np.newaxis, :, :, :]))
            all_x_add = np.concatenate((all_x_add, all_x), axis=1)

        for filename in os.listdir(inputpath_6):
            df = pd.read_csv(inputpath_6 + filename, header=0, parse_dates=['Time'], date_parser=timeparser)
            s_dataset, all_x, id = df_to_x_m_d(df=df, inputdict=inputdict, size=size, id_posistion=id_posistion,
                                               split=input_length)

            dataset = np.concatenate((dataset, s_dataset[np.newaxis, :, :, :]))
            all_x_add = np.concatenate((all_x_add, all_x), axis=1)

        for filename in os.listdir(inputpath_7):
            df = pd.read_csv(inputpath_7 + filename, header=0, parse_dates=['Time'], date_parser=timeparser)
            s_dataset, all_x, id = df_to_x_m_d(df=df, inputdict=inputdict, size=size, id_posistion=id_posistion, split=input_length)

            dataset = np.concatenate((dataset, s_dataset[np.newaxis, :, :, :]))
            all_x_add = np.concatenate((all_x_add, all_x), axis=1)

        for filename in os.listdir(inputpath_8):
            df = pd.read_csv(inputpath_8 + filename, header=0, parse_dates=['Time'], date_parser=timeparser)
            s_dataset, all_x, id = df_to_x_m_d(df=df, inputdict=inputdict, size=size, id_posistion=id_posistion,
                                               split=input_length)

            dataset = np.concatenate((dataset, s_dataset[np.newaxis, :, :, :]))
            all_x_add = np.concatenate((all_x_add, all_x), axis=1)

        for filename in os.listdir(inputpath_9):
            df = pd.read_csv(inputpath_9 + filename, header=0, parse_dates=['Time'], date_parser=timeparser)
            s_dataset, all_x, id = df_to_x_m_d(df=df, inputdict=inputdict, size=size, id_posistion=id_posistion,
                                               split=input_length)

            dataset = np.concatenate((dataset, s_dataset[np.newaxis, :, :, :]))
            all_x_add = np.concatenate((all_x_add, all_x), axis=1)

        for filename in os.listdir(inputpath_10):
            df = pd.read_csv(inputpath_10 + filename, header=0, parse_dates=['Time'], date_parser=timeparser)
            s_dataset, all_x, id = df_to_x_m_d(df=df, inputdict=inputdict, size=size, id_posistion=id_posistion,
                                               split=input_length)

            dataset = np.concatenate((dataset, s_dataset[np.newaxis, :, :, :]))
            all_x_add = np.concatenate((all_x_add, all_x), axis=1)

        for filename in os.listdir(inputpath_11):
            df = pd.read_csv(inputpath_11 + filename, header=0, parse_dates=['Time'], date_parser=timeparser)
            s_dataset, all_x, id = df_to_x_m_d(df=df, inputdict=inputdict, size=size, id_posistion=id_posistion,
                                               split=input_length)

            dataset = np.concatenate((dataset, s_dataset[np.newaxis, :, :, :]))
            all_x_add = np.concatenate((all_x_add, all_x), axis=1)

        for filename in os.listdir(inputpath_12):
            df = pd.read_csv(inputpath_12 + filename, header=0, parse_dates=['Time'], date_parser=timeparser)
            s_dataset, all_x, id = df_to_x_m_d(df=df, inputdict=inputdict, size=size, id_posistion=id_posistion,
                                               split=input_length)

            dataset = np.concatenate((dataset, s_dataset[np.newaxis, :, :, :]))
            all_x_add = np.concatenate((all_x_add, all_x), axis=1)

        for filename in os.listdir(inputpath_13):
            df = pd.read_csv(inputpath_13 + filename, header=0, parse_dates=['Time'], date_parser=timeparser)
            s_dataset, all_x, id = df_to_x_m_d(df=df, inputdict=inputdict, size=size, id_posistion=id_posistion,
                                               split=input_length)

            dataset = np.concatenate((dataset, s_dataset[np.newaxis, :, :, :]))
            all_x_add = np.concatenate((all_x_add, all_x), axis=1)


        dataset = dataset[1:, :, :, :]
        all_x_add = all_x_add[:, 1:]

        train_proportion = 0.8
        train_index = int(all_x_add.shape[1] * train_proportion)
        train_x = all_x_add[:, :train_index]

        x_mean = get_mean(train_x)
        x_std = get_std(train_x)

        x_mean = np.asarray(x_mean)
        x_std = np.asarray(x_std)

        dataset = dataset_normalize(dataset=dataset, mean=x_mean, std=x_std)

        nor_mean, nor_median, nor_std, nor_var = normalize_chk(dataset)

        np.save('P12saved/x_mean_aft_nor', nor_mean)
        np.save('P12saved/x_median_aft_nor', nor_median)
        np.save('P12saved/dataset.npy', dataset)

        t_dataset = np.load('P12saved/dataset.npy')
        print(t_dataset.shape)

        A_outcomes = pd.read_csv('../../P12data/rawdata/Outcomes-djy.txt')
        B_outcomes = pd.read_csv('../../P12data/rawdata/Outcomes-gxx.txt')
        C_outcomes = pd.read_csv('../../P12data/rawdata/Outcomes-jsw.txt')
        D_outcomes = pd.read_csv('../../P12data/rawdata/Outcomes-jxt.txt')
        E_outcomes = pd.read_csv('../../P12data/rawdata/Outcomes-lfz.txt')
        F_outcomes = pd.read_csv('../../P12data/rawdata/Outcomes-lh.txt')
        G_outcomes = pd.read_csv('../../P12data/rawdata/Outcomes-mtq.txt')
        H_outcomes = pd.read_csv('../../P12data/rawdata/Outcomes-qc.txt')
        I_outcomes = pd.read_csv('../../P12data/rawdata/Outcomes-qjw.txt')
        J_outcomes = pd.read_csv('../../P12data/rawdata/Outcomes-zdf.txt')
        K_outcomes = pd.read_csv('../../P12data/rawdata/Outcomes-zhs.txt')
        L_outcomes = pd.read_csv('../../P12data/rawdata/Outcomes-zwl.txt')
        M_outcomes = pd.read_csv('../../P12data/rawdata/Outcomes-zzt.txt')

        y_a_outcomes = df_to_y1(A_outcomes)
        y_b_outcomes = df_to_y1(B_outcomes)
        y_c_outcomes = df_to_y1(C_outcomes)
        y_d_outcomes = df_to_y1(D_outcomes)
        y_e_outcomes = df_to_y1(E_outcomes)
        y_f_outcomes = df_to_y1(F_outcomes)
        y_g_outcomes = df_to_y1(G_outcomes)
        y_h_outcomes = df_to_y1(H_outcomes)
        y_i_outcomes = df_to_y1(I_outcomes)
        y_j_outcomes = df_to_y1(J_outcomes)
        y_k_outcomes = df_to_y1(K_outcomes)
        y_l_outcomes = df_to_y1(L_outcomes)
        y_m_outcomes = df_to_y1(M_outcomes)


        y1_outcomes = np.concatenate((y_a_outcomes, y_b_outcomes, y_c_outcomes,y_d_outcomes,y_e_outcomes,y_f_outcomes,y_g_outcomes,y_h_outcomes,y_i_outcomes,y_j_outcomes,y_k_outcomes,y_l_outcomes,y_m_outcomes))
        print(y1_outcomes.shape)
        np.save('P12saved/y1_out', y1_outcomes)

