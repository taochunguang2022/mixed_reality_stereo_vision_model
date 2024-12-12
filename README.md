# mixed_reality_stereo_vision_model
#### Authors: Yan Wu (wuyan@cust.edu.cn), Chunguang Tao(2022100987@mails.cust.edu.cn), Qi Li(liqi@cust.edu.cn)
## Overview
This repository contains processed multimodal datasets and implementation model validation dataset codes. Regarding the relevant code for the design of the experimental paradigm of this study, please refer to the experimental folder. "P12" refers to a dataset that consists of all 12 types of peripheral physiological features. "P36" expands on P12 by incorporating brain network features from 24 channels, resulting in a dataset that combines both peripheral physiological and brain network features. The multimodal feature dataset following the format of [the PhysioNet Mortality Prediction Challenge 2012 dataset](https://physionet.org/content/challenge-2012/1.0.0/) The overall framework is as follows:

  ![image](https://github.com/taochunguang2022/mixed_reality_stereo_vision_model/blob/main/overview.jpg)
## Experiment
Every participant was mandated to fully view 2 restful scenes and 15 movement scenes. Following each movement scene, a rating scene emerged to evaluate the content watched. Each movement scene consisted of 20 trials of reciprocal periodic movements at a depth of 8m. Participants rated their current comfort level on the same 5-point scale. If the participant maintained their gaze on any square for 3 seconds, the square would turn red, indicating that the subjective evaluation was successful, and the experiment would proceed to the next scene. Otherwise, if the participant’s gaze shifted to another square the timer would reset, and the square would turn green. Data collected before and after watching the entire 15 blocks were labeled “comfort” and “fatigue”, respectively, and were used for feature extraction. All experiment protocol scripts were developed in Visual Studio 2022. The experimental scenarios were built using Unity Editor 2021.3.4f1c1 and Mixed Reality Toolkit version 2.8.3.
## Datasets
The P12 dataset primarily originates from data features extracted from the ErgoLAB wireless ECG sensor and the ErgoLAB wearable finger sensor. The P36 dataset expands upon P12 by adding 24-channel brain network features. Specifically, as follows:
  ![image](https://github.com/taochunguang2022/mixed_reality_stereo_vision_model/blob/main/datasets.jpg)
### General Descriptors
The beginning of each record was set to 00:00. They represent general descriptors.
- RecordID (a unique record)
- Gender (0: male, or 1: female)
### Outcome-related Descriptors
- RecordID (a unique record)
- fatigue (0: no, or 1: yes)
### P12data
"P12" refers to a dataset that consists of all 12 types of peripheral physiological features. These 12 variables are randomly distributed at times other than 00:00.
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
### P36data
"P36" expands on P12 by incorporating brain network features from 24 channels, resulting in a dataset that combines both peripheral physiological and brain network features.
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
  
