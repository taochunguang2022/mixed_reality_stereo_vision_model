import lightgbm as lgb
import numpy as np
from sklearn.metrics import roc_auc_score
from sklearn.preprocessing import StandardScaler
from imputations import *

dataset = 'P36'  # possible values: 'P12', 'P36'
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

if dataset == 'P36' or dataset == 'P12':
    T, F = features[0]['arr'].shape
    feature_np = np.zeros((len(features), T, F))
    for i in range(len(features)):
        feature_np[i] = features[i]['arr']


n_sensors = feature_np.shape[-1]
AUC_list = []
scaler = StandardScaler()

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
    train_data = data_fea_label[: int(0.8 * n_seg)]
    test_data = data_fea_label[int(0.8 * n_seg):]

    no_fea_long = train_data.shape[-1] - 1

    np.random.shuffle(train_data)
    np.random.shuffle(test_data)

    feature_train = train_data[:, :no_fea_long]
    feature_test = test_data[:, :no_fea_long]
    # 特征归一化
    feature_train = scaler.fit_transform(feature_train)
    feature_test = scaler.transform(feature_test)

    label_train = train_data[:, no_fea_long:no_fea_long + 1].squeeze(-1)
    label_test = test_data[:, no_fea_long:no_fea_long + 1].squeeze(-1)

    # 使用LightGBM进行训练
    lgb_train = lgb.Dataset(feature_train, label_train)
    lgb_eval = lgb.Dataset(feature_test, label_test, reference=lgb_train)

    params = {
        'boosting_type': 'gbdt',
        'objective': 'binary',
        'metric': 'auc',
        'num_leaves': 31,
        'learning_rate': 0.05,
        'feature_fraction': 0.9
    }

    gbm = lgb.train(params,
                    lgb_train,
                    num_boost_round=100,
                    valid_sets=lgb_eval,
                    callbacks=[lgb.early_stopping(stopping_rounds=10)],
                    )

    y_pred = gbm.predict(feature_test, num_iteration=gbm.best_iteration)

    if dataset == 'P36' or dataset == 'P12':
        auc_score = roc_auc_score(label_test, y_pred)


    print('Feature:', feature_ji, '| AUC: %.4f' % auc_score)
    AUC_list.append(auc_score)

sorted_sensor = np.argsort(np.array(AUC_list))
sensor_descending = sorted_sensor[::-1]
print(sensor_descending)

if dataset == 'P36' or dataset == 'P12':
    labels_names = np.load(labels_names_path, allow_pickle=True)


indices_with_names = np.array([[ind, labels_names[ind]] for ind in sensor_descending])

