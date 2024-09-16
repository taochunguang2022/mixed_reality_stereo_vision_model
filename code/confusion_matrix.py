import itertools
import numpy as np
import matplotlib.pyplot as plt
from sklearn.metrics import confusion_matrix

def plot_confusion_matrix(y_true, y_pred, sub, title = "Confusion matrix - Classification",
                          cmap=plt.cm.Blues, save_flg=True):
    # 检查 y_pred 和 y_true 是否是 PyTorch tensors
    if hasattr(y_pred, 'cpu') and hasattr(y_pred, 'detach'):
        y_pred = y_pred.cpu().detach().numpy()
    if hasattr(y_true, 'cpu') and hasattr(y_true, 'detach'):
        y_true = y_true.cpu().detach().numpy()
    classes = [str(i) for i in range(2)]
    labels = range(2)

    cm = confusion_matrix(y_true, y_pred, labels=labels)
    cm = cm.astype('float') / cm.sum(axis=0) #更改显示precision，需要按列归一化混淆矩阵
    plt.figure(figsize=(14, 12))
    plt.imshow(cm, interpolation='nearest', cmap=cmap)
    plt.title(title, fontsize=40)
    plt.colorbar()
    tick_marks = np.arange(len(classes))
    plt.xticks(tick_marks, classes, fontsize=20)
    plt.yticks(tick_marks, classes, fontsize=20)

    # print('Confusion matrix, without normalization')

    thresh = cm.max() / 2.
    for i, j in itertools.product(range(cm.shape[0]), range(cm.shape[1])):
        plt.text(j, i, format(cm[i, j], '.2f'),
                 horizontalalignment="center",
                 color="white" if cm[i, j] > thresh else "black",
                 fontsize=30)

    plt.ylabel('True label', fontsize=30)
    plt.xlabel('Predicted label', fontsize=30)

    if save_flg:
        plt.savefig("confusion_matrix" + str(sub) + ".png")

    # plt.show()


