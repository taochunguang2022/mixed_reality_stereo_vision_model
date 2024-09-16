import json
import numpy as np
import pandas as pd
import matplotlib.pyplot as plt

P_list = np.load('../processed_data/P_list.npy', allow_pickle=True)
arr_outcomes = np.load('../processed_data/arr_outcomes.npy', allow_pickle=True)

ts_params = np.load('../processed_data/ts_params.npy', allow_pickle=True)
static_params = np.load('../processed_data/static_params.npy', allow_pickle=True)

print('number of samples: ', len(P_list))
print(len(ts_params), ts_params)
print(len(static_params), static_params)

n = len(P_list)
max_tmins = 48 * 60
len_ts = []

for ind in range(n):  # for each patient
    ts = P_list[ind]['ts']
    unq_tmins = []
    for sample in ts:  # for each instance (time point)
        current_tmin = sample[2]
        if (current_tmin not in unq_tmins) and (current_tmin < max_tmins):
            unq_tmins.append(current_tmin)
    len_ts.append(len(unq_tmins))
print('max unique time series length:', np.max(len_ts))  # 最多214个时刻点

extended_static_list = ['Gender=0','Gender=1']
np.save('../processed_data/extended_static_params.npy', extended_static_list)

"""Group all patient time series into arrays"""
n = len(P_list)
max_len = 215
F = len(ts_params)
PTdict_list = []
max_hr = 0
for ind in range(n):
    ID = P_list[ind]['id']
    static = P_list[ind]['static']
    ts = P_list[ind]['ts']

    # find unique times
    unq_tmins = []
    for sample in ts:
        current_tmin = sample[2]
        if (current_tmin not in unq_tmins) and (current_tmin < max_tmins):
            unq_tmins.append(current_tmin)
    unq_tmins = np.array(unq_tmins)  # 计算每个记录时刻点的个数

    # one-hot encoding of categorical static variables
    extended_static = [0, 0]
    if static[0] == 0:
        extended_static[0] = 1
    elif static[0] == 1:
        extended_static[1] = 1


    # construct array of maximal size
    Parr = np.zeros((max_len, F))
    Tarr = np.zeros((max_len, 1))

    # for each time measurement find index and store
    for sample in ts:
        tmins = sample[2]
        param = sample[-2]
        value = sample[-1]
        if tmins < max_tmins:
            time_id = np.where(tmins == unq_tmins)[0][0]
            param_id = np.where(ts_params == param)[0][0]
            Parr[time_id, param_id] = value  # 每个时刻点所对应的全部特征 时刻点*特征
            Tarr[time_id, 0] = unq_tmins[time_id]

    length = len(unq_tmins)

    # construct dictionary
    my_dict = {'id': ID, 'static': static, 'extended_static': extended_static, 'arr': Parr, 'time': Tarr,     # Parr 215*12  Tarr:时间位置编码
               'length': length}

    # add array into list
    PTdict_list.append(my_dict)

print(len(PTdict_list))
np.save('../processed_data/PTdict_list.npy', PTdict_list)
print('PTdict_list.npy saved', PTdict_list[0].keys())
exit(-1)
