import pandas as pd
import random

from sklearn.linear_model import LogisticRegression
from sklearn.discriminant_analysis import LinearDiscriminantAnalysis
from sklearn.neighbors import KNeighborsClassifier
from sklearn.naive_bayes import GaussianNB
from sklearn.tree import DecisionTreeClassifier
from sklearn.svm import SVC
from sklearn.neural_network import MLPClassifier

from sklearn.model_selection import cross_validate
from sklearn.model_selection import train_test_split
from sklearn import preprocessing
from sklearn.model_selection import cross_val_score
import pickle

import matplotlib.pyplot as plt
import matplotlib
from mpl_toolkits.mplot3d import Axes3D

df_train = pd.read_csv("./data_set_test.csv")

#shuffle
df_train = df_train.sample(frac=1).reset_index(drop=True)

#create new columns
#total crouchtime + walking time
df_train['Crouch_time_n_walking_time'] = df_train.apply(lambda row: row.walking_time + row.crouch_time, axis = 1)
df_train['movement_time'] = df_train.apply(lambda row: row.walking_time + row.crouch_time + row.running_time, axis = 1)
df_train['avg_Crouch_time_+_walking_time'] = df_train.apply(lambda row: row.Crouch_time_n_walking_time / row.movement_time, axis = 1)
df_train['avg_running_time'] = df_train.apply(lambda row: row.running_time / row.movement_time, axis = 1)
df_train['avg_number_of_jumps'] = df_train.apply(lambda row: row.number_of_jumps / row.movement_time, axis = 1)

y = list(df_train['is_aggressive'])

#normalilze
# X = df_train[['running_time', 'number_of_jumps', 'Crouch_time_n_walking_time']].to_numpy().tolist()
X = df_train[['avg_running_time', 'avg_number_of_jumps', 'avg_Crouch_time_+_walking_time']].to_numpy().tolist()
min_max_scaler = preprocessing.MinMaxScaler()
X_scaled = min_max_scaler.fit_transform(X)

columns = list(df_train.columns.values)
print(columns)

# i = 6
# j = 8
# k = 11
# fig = plt.figure(figsize=(i,i))
# x1 = list(df_train[df_train["is_aggressive"]==0][columns[i]])
# y1 = list(df_train[df_train["is_aggressive"]==0][columns[j]])
# z1 = list(df_train[df_train["is_aggressive"]==0][columns[k]])

# x2 = list(df_train[df_train["is_aggressive"]==1][columns[i]])
# y2 = list(df_train[df_train["is_aggressive"]==1][columns[j]])
# z2 = list(df_train[df_train["is_aggressive"]==1][columns[k]])
# data = ((x1,y1,z1), (x2,y2,z2))
# colors = ("red", "blue")
# groups = ("passive", "aggressive")   
# ax = fig.add_subplot(1, 1, 1, projection='3d')
# # ax = Axes3D(ax_)
# for data, color, group in zip(data, colors, groups):
#     x, y, z = data
#     ax.scatter(x, y, z, c=color, label=group)
# # plt.title('\''+ columns[i] + '\' vs \'' + columns[j] + '\' vs \'' + columns[k] + '\'')
# # ax.set_xlabel(columns[i])
# # ax.set_ylabel(columns[j])
# # ax.set_zlabel(columns[k])
# c1 = "x"
# c2 = "y"
# c3 = "z"
# # plt.title('\''+ c1 + '\' vs \'' + c2 + '\' vs \'' + c3 + '\'')
# ax.set_xlabel(c1, fontsize=20)
# ax.set_ylabel(c2, fontsize=20)
# ax.set_zlabel(c3, fontsize=20)
# plt.legend()
# plt.show()



# df_test = pd.read_csv("./data_set_new_test.csv")

# #shuffle
# # df_test = df_test.sample(frac=1).reset_index(drop=True)

# #create new columns
# #total crouchtime + walking time
# df_test['Crouch_time_n_walking_time'] = df_test.apply(lambda row: row.walking_time + row.crouch_time, axis = 1)
# df_test['movement_time'] = df_test.apply(lambda row: row.walking_time + row.crouch_time + row.running_time, axis = 1)
# df_test['avg_Crouch_time_+_walking_time'] = df_test.apply(lambda row: row.Crouch_time_n_walking_time / row.movement_time, axis = 1)
# df_test['avg_running_time'] = df_test.apply(lambda row: row.running_time / row.movement_time, axis = 1)
# df_test['avg_number_of_jumps'] = df_test.apply(lambda row: row.number_of_jumps / row.movement_time, axis = 1)

# y_test = list(df_test['is_aggressive'])

# #normalilze
# # X_test = df_test[['running_time', 'number_of_jumps', 'Crouch_time_n_walking_time']].to_numpy().tolist()
# X_test = df_test[['avg_running_time', 'avg_number_of_jumps', 'avg_Crouch_time_+_walking_time']].to_numpy().tolist()
# min_max_scaler = preprocessing.MinMaxScaler()
# X_test_scaled = min_max_scaler.fit_transform(X_test)

X_train, X_test, y_train, y_test = train_test_split(X_scaled, y, test_size=0.20, random_state=42)

# clfs = [MLPClassifier(solver='adam', alpha=1e-5,hidden_layer_sizes=(5,5),random_state=1),
clfs =  [
		LogisticRegression(),
		LinearDiscriminantAnalysis(),
		KNeighborsClassifier(),
		GaussianNB(),
		DecisionTreeClassifier(),
		SVC()]

for clf in clfs:
	# clf.fit(X_scaled, y)
	scores = cross_val_score(clf, X_scaled, y, cv=5)
	# cv_results = cross_validate(clf, X, y, cv=5) 
	# print("Fit scores: {}".format(cv_results['test_score']))
	# print(X, y)
	# print(clf.score(X_test, y_test, sample_weight=None))
	print(scores)

clf = LogisticRegression()
clf.fit(X_scaled, y)
print(clf.predict_proba([[0.5, 0.5, 0.5]])[0][1])

filename = 'finalized_model.model'
pickle.dump(clf, open(filename, 'wb'))
 
# some time later...
 
# # load the model from disk
# loaded_model = pickle.load(open(filename, 'rb'))
# result = loaded_model.score(X_test, Y_test)
# print(result)