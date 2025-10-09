# mixed_reality_stereo_vision_model
#### Authors: Yan Wu (wuyan@cust.edu.cn), Chunguang Tao(2022100987@mails.cust.edu.cn), Qi Li(liqi@cust.edu.cn)
## Overview
This repository contains codes for model implementation. For the relevant codes pertaining to the experimental paradigm design of this study, please refer to the experimental folder. For the processed multimodal datasets, pleses visit [Figshare](https://figshare.com/articles/dataset/mixed_reality_stereo_vision_dataset/28039478). The multimodal datasets contain "P12" and "P36" datasets. "P12" refers to a dataset that consists of all 12 types of peripheral physiological features. "P36" expands on P12 by incorporating brain network features from 24 channels, resulting in a dataset that combines both peripheral physiological and brain network features. The multimodal feature dataset following the format of [the PhysioNet Mortality Prediction Challenge 2012 dataset](https://physionet.org/content/challenge-2012/1.0.0/) . The overall framework is as follows:

  ![image](https://github.com/taochunguang2022/mixed_reality_stereo_vision_model/blob/main/overview.jpg)
## Experiment
Every participant was mandated to fully view 2 restful scenes and 15 movement scenes. Following each movement scene, a rating scene emerged to evaluate the content watched. Each movement scene consisted of 20 trials of reciprocal periodic movements at a depth of 8m. Participants rated their current comfort level on the same 5-point scale. If the participant maintained their gaze on any square for 3 seconds, the square would turn red, indicating that the subjective evaluation was successful, and the experiment would proceed to the next scene. Otherwise, if the participant’s gaze shifted to another square the timer would reset, and the square would turn green. Data collected before and after watching the entire 15 blocks were labeled “comfort” and “fatigue”, respectively, and were used for feature extraction. All experiment protocol scripts were developed in Visual Studio 2022. The experimental scenarios were built using Unity Editor 2021.3.4f1c1 and Mixed Reality Toolkit version 2.8.3.
## Datasets
The P12 dataset primarily originates from data features extracted from the ErgoLAB wireless ECG sensor and the ErgoLAB wearable finger sensor. The P36 dataset expands upon P12 by adding 24-channel brain network features. The brain network features main focus to extract the mean values of betweenness centrality (BC), nodal efficiency (NE), the clustering coefficient (CC) from the corpus. Specifically, as follows:
  ![image](https://github.com/taochunguang2022/mixed_reality_stereo_vision_model/blob/main/datasets.jpg)
### General Descriptors
The beginning of each record was set to 00:00. They represent general descriptors.
- RecordID (a unique record)
- Gender (0: male, or 1: female)
### Outcome-related Descriptors
- RecordID (a unique record)
- fatigue (0: no, or 1: yes)
### Time Series Variables
- chestHR
  - Heart rate values for ECG signals
- chestIBI
  - All R-R intervals during the recording period
- chestRPeak
  - R-wave peaks in the QRS wave group of the ECG
- chestRI
  - Respiratory signals the length of a complete respiratory cycle
- chestRPeaks
  - Peak points in the respiratory waveform
- chestRR
  - Time difference between the peaks of two consecutive
- fingerEDA
  - Primary skin electrical signal
- fingerEDASC
  - Overall skin electrical signals, including slow and fast changing portions
- fingerEDATD
  - The slow-varying portion of the skin electrical signal
- fingerEDAPD
  - Rapidly changing portion of the skin electrical signal
- fingerSpO2
  - Oxygen saturation test in finger blood
- fingerSKT
  - Finger skin temperature   
- Frontal
  - Fp1, Fp2, AF3, AF4, F7, Fz, F8, FC5, FC6
- Temporal
  - FT7, FT8
- Central
  - C3, Cz, C4, CP3, CP4
- Parietal
  - P3, Pz, P4, PO3, PO4
- Occipital
  - O1, Oz, O2

## RainDrop Model Architecture

All classification model scripts were developed in Python 3.9, using PyTorch 1.12.0 with CUDA 11.3 and torch-geometric version 2.3.1, as the models primarily involve graph neural networks (GNN). Taking the RainDrop model as an example, the architecture diagram is as follows:
![image](https://github.com/taochunguang2022/mixed_reality_stereo_vision_model/blob/main/RainDrop.jpg)

To assess the effectiveness of the RainDrop model in processing multimodal data for classification tasks, the P36 dataset is fed into this model. This table details the RainDrop model’s architecture, including layers/variables, input/output dimensions, and function descriptions. It covers stages from initial input and preprocessing to GNN layers (the first and second layers) and output processing, explaining how time series features, brain network features, and timestamps from the P36 dataset are processed to finally output classification results.

## 1. Initial Inputs
| Layer/Variable | Input Dimension | Output Dimension | Function Description          |
|----------------|-----------------|------------------|-------------------------------|
| `src`          | (206, 128, 36)  | —                | Time series feature input     |
| `brainnet`     | (128, 4)        | —                | Brain network feature input   |
| `times`        | (206, 128)      | —                | Timestamp input               |
| `lengths`      | (128)           | —                | Valid time steps per sample   |


## 2. Preprocessing Layer
| Layer/Variable          | Input Dimension | Output Dimension | Function Description                |
|-------------------------|-----------------|------------------|-------------------------------------|
| `missing_mask`          | (206, 128, 36)  | (206, 128, 36)   | Extract missing value mask from `src` |
| `src` (valid features)  | (206, 128, 36)  | (206, 128, 36)   | Extract valid time series from `src`  |
| `src(repeat_interleave)`| (206, 128, 36)  | (206, 128, 72)   | Expand feature dimension             |


## 3. GNN Layers
<details>
<summary>First GNN Layer</summary>

| Layer/Variable   | Input Dimension | Output Dimension | Function Description                |
|------------------|-----------------|------------------|-------------------------------------|
| `stepdata`       | (206, 72)       | (36, 412)        | Reshape for single-sample time series |
| `edge_index`     | —               | (2, 1296)        | Graph edge index (input)            |
| `edge_weights`   | (36, 36)        | (1296)           | Graph edge weights (input)          |
| `query-key`      | (1296, 412)     | (1296, 1, 412)   | Linear transform for node features  |
| `alpha` (attention) | (1296, 1, 412) | (1296, 1)        | Compute edge attention weights      |
| `out` (propagated) | (1296, 1, 412) | (36, 412)        | Aggregate neighbor features via attention |

</details>

<details>
<summary>Second GNN Layer</summary>

| Layer/Variable       | Input Dimension | Output Dimension | Function Description                |
|----------------------|-----------------|------------------|-------------------------------------|
| `stepdata` (input)   | (36, 412)       | (36, 412)        | Node features from first GNN output |
| `edge_index` (input) | (2, 1296)       | (2, 648)         | Edge index (top 50% high-weight edges after pruning) |
| `edge_weights` (input) | (1296)       | (648)            | Corresponding pruned edge weights   |
| `gamma` (temporal attn) | (1296, 206) | (648, 412)       | Compute temporal attention weights with time encoding |
| `out` (propagated)   | (648, 1, 412)   | (36, 412)        | Aggregate neighbors with pruned edges/attention |

</details>


## 4. Output Processing
| Layer/Variable          | Input Dimension | Output Dimension | Function Description                |
|-------------------------|-----------------|------------------|-------------------------------------|
| `stepdata` (reshaped)   | (36, 206, 2)    | (206, 128, 72)   | Reshape second GNN output to time series format |
| `TransformerEncoder`    | (206, 128, 72)  | (206, 128, 72)   | Model sequence in time dimension    |
| `output` (with PE)      | (206, 128, 72)  | (206, 128, 88)   | Concatenate output features with time encoding |
| `output` (aggregated)   | (206, 128, 88)  | (128, 88)        | Mean aggregation via valid length mask |
| `output` (with brainnet)| (128, 88)       | (128, 124)       | Concatenate with brain network features |
| `mlp`                   | (128, 124)      | (128, 2)         | Output final classification via MLP |

## RainDrop Model Output

The confusion matrix results obtained after five-fold cross-validation are illustrated in the figures below.
![image](https://github.com/taochunguang2022/mixed_reality_stereo_vision_model/blob/main/output.jpg)

For this classification task, fatigue (Label 1) is defined as the positive class, and comfort (Label 0) as the negative class. Detailed performance metrics for each fold (corresponding to Figures (a)–(e)) are summarized as follows:
#### Figure (a)
Normalized ratios of True Positive (TP), True Negative (TN), False Positive (FP), and False Negative (FN) are 0.94, 0.95, 0.06, and 0.05, respectively. Overall classification accuracy: 94.77%.
#### Figure (b)
Normalized ratios of TP, TN, FP, and FN are 0.92, 0.94, 0.08, and 0.06, respectively. Overall classification accuracy: 92.90%.
#### Figure (c)
Normalized ratios of TP, TN, FP, and FN are 0.92, 0.95, 0.08, and 0.05, respectively. Overall classification accuracy: 93.55%.
#### Figure (d)
Normalized ratios of TP, TN, FP, and FN are 0.93, 0.95, 0.07, and 0.05, respectively. Overall classification accuracy: 94.11%.
#### Figure (e)
Normalized ratios of TP, TN, FP, and FN are 0.95, 0.95, 0.05, and 0.05, respectively. Overall classification accuracy: 94.95%.

Across all five folds, the model maintains consistently high performance—with classification accuracy ranging from 92.90% to 94.95%—and exhibits strong ability to distinguish between fatigued (Label 1) and comfortable (Label 0) states.

### Requirements
All models have tested using Python 3.9.

To have consistent libraries and their versions, you can install needed dependencies for this project running the following command:

```bash
pip install -r requirements.txt
```

### Unimodal Assess
Considering the various time series variables mentioned above, train models using the following classifiers:
- LGBM (Light Gradient Boosting Machine)
- MLP (Multi-Layer Perceptron)
- RF (Random Forest)
- SVM (Support Vector Machine)
- XB (Extreme Gradient Boosting)

### Multimodal Characterization Assess
The RAINDROP model is very useful for our research. We provide the code for the RAINDROP model as well as the following baseline models: 
- Transformer
- GRU-D, MTGNN
- SeFT
- DGM2

## Cite
```bibtex
@inproceedings{zhang2021graph,
  Title = {Graph-Guided Network For Irregularly Sampled Multivariate Time Series},
  author = {Zhang, Xiang and Zeman, Marko and Tsiligkaridis, Theodoros and Zitnik, Marinka},
  booktitle = {International Conference on Learning Representations, ICLR},
  year = {2022}
}
