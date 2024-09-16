import pickle
from sklearn.neural_network import MLPClassifier
import numpy as np
from sklearn.metrics import roc_auc_score
from sklearn.metrics import classification_report
from sklearn.preprocessing import scale
from imputations import *

dataset = 'P12'     # possible values: 'P12', 'P36'
print('Dataset used: ', dataset)

if dataset == 'P36':
    labels_path = '../../P36data/processed_data/arr_outcomes.npy'
    labels_names_path = '../../P36data/processed_data/ts_params.npy'
    features_path = '../../P36data/processed_data/PTdict_list.npy'
elif dataset == 'P12':
    labels_path = '../../P12data/processed_data/arr_outcomes.npy'
    labels_names_path = '../../P12data/processed_data/ts_params.npy'
    features_path = '../../P12data/processed_data/PTdict_list.npy'

labels = np.load(labels_path, allow_pickle=True)
if dataset == 'P36' or dataset == 'P12' :
    labels_np = labels[:, -1].reshape([-1, 1])


features = np.load(features_path, allow_pickle=True)

if dataset == 'P36' or dataset == 'P12' :
    T, F = features[0]['arr'].shape
    feature_np = np.zeros((len(features), T, F))
    for i in range(len(features)):
        feature_np[i] = features[i]['arr']


n_sensors = feature_np.shape[-1]
AUC_list = []
for f in range(n_sensors):
    feature_ji = f

    feature_1 = feature_np[:, :, feature_ji]
    # 对每个特征时序数据缺失值进行均值插补
    feature_1 = mean_impute(feature_1)
    # 特征与标签的合并
    data_fea_label = np.hstack((feature_1, labels_np))

    n_seg = data_fea_label.shape[0]
    np.random.shuffle(data_fea_label)
    # 将数据随机打乱后划分为训练集与测试集（8：2）
    train_data = data_fea_label[: int(0.8*n_seg)]
    test_data = data_fea_label[int(0.8*n_seg):]

    no_fea_long = train_data.shape[-1] - 1

    np.random.shuffle(train_data)
    np.random.shuffle(test_data)

    feature_train = train_data[:, :no_fea_long]
    feature_test = test_data[:, :no_fea_long]
    # 特征归一化
    feature_train = scale(feature_train, axis=0)
    feature_test = scale(feature_test, axis=0)

    label_train = train_data[:, no_fea_long:no_fea_long + 1].squeeze(-1)
    label_test = test_data[:, no_fea_long:no_fea_long + 1].squeeze(-1)

    # 训练多层感知器模型
    mlp = MLPClassifier(max_iter=1000).fit(feature_train, label_train)

    # 评估模型性能
    svc_acc = mlp.score(feature_test, label_test)
    svc_acc_train = mlp.score(feature_train, label_train)

    svc_result = mlp.predict(feature_test)

    if dataset == 'P36' or dataset == 'P12':
        auc_score = roc_auc_score(label_test, mlp.predict_proba(feature_test)[:, 1])

    print('Feature:', feature_ji, '| ACC: %.4f' % svc_acc, '| AUC: %.4f' % auc_score)
    AUC_list.append(auc_score)

sorted_sensor = np.argsort(np.array(AUC_list))
sensor_descending = sorted_sensor[::-1]
print(sensor_descending)

if dataset == 'P36' or dataset == 'P12' :
    labels_names = np.load(labels_names_path, allow_pickle=True)

indices_with_names = np.array([[ind, labels_names[ind]] for ind in sensor_descending])

