import pandas as pd
import numpy as np
import matplotlib.pyplot as plt
import os

df_outcomes_a = pd.read_csv('../rawdata/Outcomes-djy.txt', sep=",", header=0,
                            names=["RecordID","fatigue"])
df_outcomes_b = pd.read_csv('../rawdata/Outcomes-gxx.txt', sep=",", header=0,
                            names=["RecordID","fatigue"])
df_outcomes_c = pd.read_csv('../rawdata/Outcomes-jsw.txt', sep=",", header=0,
                            names=["RecordID","fatigue"])
df_outcomes_d = pd.read_csv('../rawdata/Outcomes-jxt.txt', sep=",", header=0,
                            names=["RecordID","fatigue"])
df_outcomes_e = pd.read_csv('../rawdata/Outcomes-lfz.txt', sep=",", header=0,
                            names=["RecordID","fatigue"])
df_outcomes_f = pd.read_csv('../rawdata/Outcomes-lh.txt', sep=",", header=0,
                            names=["RecordID","fatigue"])
df_outcomes_g = pd.read_csv('../rawdata/Outcomes-mtq.txt', sep=",", header=0,
                            names=["RecordID","fatigue"])
df_outcomes_h = pd.read_csv('../rawdata/Outcomes-qc.txt', sep=",", header=0,
                            names=["RecordID","fatigue"])
df_outcomes_i = pd.read_csv('../rawdata/Outcomes-qjw.txt', sep=",", header=0,
                            names=["RecordID","fatigue"])
df_outcomes_j = pd.read_csv('../rawdata/Outcomes-zdf.txt', sep=",", header=0,
                            names=["RecordID","fatigue"])
df_outcomes_k = pd.read_csv('../rawdata/Outcomes-zhs.txt', sep=",", header=0,
                            names=["RecordID","fatigue"])
df_outcomes_l = pd.read_csv('../rawdata/Outcomes-zwl.txt', sep=",", header=0,
                            names=["RecordID","fatigue"])
df_outcomes_m = pd.read_csv('../rawdata/Outcomes-zzt.txt', sep=",", header=0,
                            names=["RecordID","fatigue"])
print(df_outcomes_a.head(n=1))
print(df_outcomes_b.head(n=1))
print(df_outcomes_c.head(n=1))
print(df_outcomes_d.head(n=1))
print(df_outcomes_e.head(n=1))
print(df_outcomes_f.head(n=1))
print(df_outcomes_g.head(n=1))
print(df_outcomes_h.head(n=1))
print(df_outcomes_i.head(n=1))
print(df_outcomes_j.head(n=1))
print(df_outcomes_k.head(n=1))
print(df_outcomes_l.head(n=1))
print(df_outcomes_m.head(n=1))
arr_outcomes_a = np.array(df_outcomes_a)
arr_outcomes_b = np.array(df_outcomes_b)
arr_outcomes_c = np.array(df_outcomes_c)
arr_outcomes_d = np.array(df_outcomes_d)
arr_outcomes_e = np.array(df_outcomes_e)
arr_outcomes_f = np.array(df_outcomes_f)
arr_outcomes_g = np.array(df_outcomes_g)
arr_outcomes_h = np.array(df_outcomes_h)
arr_outcomes_i = np.array(df_outcomes_i)
arr_outcomes_j = np.array(df_outcomes_j)
arr_outcomes_k = np.array(df_outcomes_k)
arr_outcomes_l = np.array(df_outcomes_l)
arr_outcomes_m = np.array(df_outcomes_m)

n_a = arr_outcomes_a.shape[0]
n_b = arr_outcomes_b.shape[0]
n_c = arr_outcomes_c.shape[0]
n_d = arr_outcomes_d.shape[0]
n_e = arr_outcomes_e.shape[0]
n_f = arr_outcomes_f.shape[0]
n_g = arr_outcomes_g.shape[0]
n_h = arr_outcomes_h.shape[0]
n_i = arr_outcomes_i.shape[0]
n_j = arr_outcomes_j.shape[0]
n_k = arr_outcomes_k.shape[0]
n_l = arr_outcomes_l.shape[0]
n_m = arr_outcomes_m.shape[0]
print('n_a = %d, n_b = %d,n_c = %d,n_d = %d,n_e = %d,n_f = %d,n_g = %d,n_h = %d,n_i = %d,n_j = %d,n_k = %d,n_l = %d,n_m = %d' % (n_a,n_b,n_c,n_d,n_e,n_f,n_g,n_h,n_i,n_j,n_k,n_l,n_m))

# merge dataframes
arr_outcomes = np.concatenate([arr_outcomes_a, arr_outcomes_b,arr_outcomes_c,arr_outcomes_d,arr_outcomes_e,arr_outcomes_f,arr_outcomes_g,arr_outcomes_h,arr_outcomes_i,arr_outcomes_j,arr_outcomes_k,arr_outcomes_l,arr_outcomes_m], axis=0)
n = arr_outcomes.shape[0]
print(arr_outcomes.shape)

y_fatigue = arr_outcomes[:,-1]
print("Percentage of fatigue: %.2f%%" % (np.sum(y_fatigue)/n*100))
print(y_fatigue.shape)

# Store outcomes in npy format
np.save('../processed_data/arr_outcomes.npy', arr_outcomes)
print('arr_outcomes.npy saved')

# extract all parameters encountered across all patients
def extract_unq_params(path):
    cnt = 0
    for f in os.listdir(path):
        file_name, file_ext = os.path.splitext(f)
        if file_ext == '.txt':
            df_temp = pd.read_csv(path+file_name+'.txt', sep=",", header=1, names=["time", "param", "value"])
            arr_data_temp = np.array(df_temp)
            params_temp = arr_data_temp[:, 1]
            if cnt==0:
                params_all = params_temp
            else:
                params_all = np.concatenate([params_all, params_temp], axis=0)
            cnt += 1
    params_all = list(params_all)
    params_all = [p for p in params_all if str(p) != 'nan']
    param_list = list(np.unique(np.array(params_all)))
    return param_list

param_list_a = extract_unq_params('../rawdata/djy/')
param_list_b = extract_unq_params('../rawdata/gxx/')
param_list_c = extract_unq_params('../rawdata/jsw/')
param_list_d = extract_unq_params('../rawdata/jxt/')
param_list_e = extract_unq_params('../rawdata/lfz/')
param_list_f = extract_unq_params('../rawdata/lh/')
param_list_g = extract_unq_params('../rawdata/mtq/')
param_list_h = extract_unq_params('../rawdata/qc/')
param_list_i = extract_unq_params('../rawdata/qjw/')
param_list_j = extract_unq_params('../rawdata/zdf/')
param_list_k = extract_unq_params('../rawdata/zhs/')
param_list_l = extract_unq_params('../rawdata/zwl/')
param_list_m = extract_unq_params('../rawdata/zzt/')
param_list = param_list_a + param_list_b + param_list_c + param_list_d + param_list_e + param_list_f + param_list_g + param_list_h + param_list_i + param_list_j + param_list_k + param_list_l + param_list_m
param_list = list(np.unique(param_list))

#  删除静态特征

param_list.remove("Gender")

print("Parameters: ", param_list)
print("Number of total parameters:", len(param_list))

# save variable names
np.save('../processed_data/ts_params.npy', param_list)
print('ts_params.npy: the names of 12 variables')

static_param_list = ['Gender']
np.save('../processed_data/static_params.npy', static_param_list)
print('save names of static descriptors: static_params.npy')


def parse_all(path):
    P_list = []    # 把时序特征全部提取好
    cnt = 0
    allfiles = os.listdir(path)
    allfiles.sort()
    for f in allfiles:
        file_name, file_ext = os.path.splitext(f)
        if file_ext == '.txt':
            df = pd.read_csv(path + file_name + '.txt', sep=",", header=1, names=["time", "param", "value"])
            df_demogr = df.iloc[0:1]
            df_data = df.iloc[1:]

            arr_demogr = np.array(df_demogr)
            arr_data = np.array(df_data)

            my_dict = {'id': file_name}
            my_dict['static'] = ([arr_demogr[0, 2]])  # 将static包装成数组,加上中括号！！！

            # time-series
            n_pts = arr_data.shape[0]
            ts_list = []
            for i in range(n_pts):  # for each line
                param = arr_data[i, 1]  # the name of variables
                if param in param_list:
                    ts = arr_data[i, 0]  # time stamp
                    hrs, mins = float(ts[0:2]), float(ts[3:5])
                    value = arr_data[i, 2]  # value of variable
                    totalmins = 60.0 * hrs + mins
                    ts_list.append((hrs, mins, totalmins, param, value))
            my_dict['ts'] = ts_list

            # append patient dictionary in master dictionary
            P_list.append(my_dict)
            cnt += 1
    return P_list

# Merge lists of patients into master list
p_list_a = parse_all('../rawdata/djy/')
p_list_b = parse_all('../rawdata/gxx/')
p_list_c = parse_all('../rawdata/jsw/')
p_list_d = parse_all('../rawdata/jxt/')
p_list_e = parse_all('../rawdata/lfz/')
p_list_f = parse_all('../rawdata/lh/')
p_list_g = parse_all('../rawdata/mtq/')
p_list_h = parse_all('../rawdata/qc/')
p_list_i = parse_all('../rawdata/qjw/')
p_list_j = parse_all('../rawdata/zdf/')
p_list_k = parse_all('../rawdata/zhs/')
p_list_l = parse_all('../rawdata/zwl/')
p_list_m = parse_all('../rawdata/zzt/')


P_list = p_list_a + p_list_b + p_list_c + p_list_d + p_list_e + p_list_f + p_list_g + p_list_h + p_list_i + p_list_j + p_list_k + p_list_l + p_list_m
print('Length of P_list', len(P_list))

np.save('../processed_data/P_list.npy', P_list)
print('P_list.npy saved')


