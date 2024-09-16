import numpy as np

def mean_impute(features):
    """
    使用均值插补缺失值
    :param features: 特征矩阵，形状为 (样本数, 时间步数)
    :return: 插补后的特征矩阵
    """ # features 4000*215
    for i in range(features.shape[1]):  # 遍历每个时间点
        feature_column = features[:, i] # 提取某个时间点所有样本的特征
        non_zero_values = feature_column[feature_column != 0]
        if len(non_zero_values) > 0:
            mean_value = np.mean(non_zero_values)  # 计算非零元素的均值
            feature_column[feature_column == 0] = mean_value  # 将缺失值（假定为0）替换为均值
        features[:, i] = feature_column #  更新特征矩阵
    return features