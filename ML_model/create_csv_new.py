import os
import datetime as dt
import csv

passive_file_path = "./Passive_new"
aggrasive_file_path = "./Aggressive_new"
paths = [passive_file_path, aggrasive_file_path]

# formatting of time string
def get_time(time):
	if time[len(time)-1] == ".":
		return time + "000"
	elif not "." in time:
		return time + ".000"
	else:
		return time

with open('data_set_new.csv', mode='w') as data_set:
	data_set_writter = csv.writer(data_set, delimiter=',')
	data_set_writter.writerow(['file_name', 'tot_num_actions', 'number_of_shots', 'number_of_succ_shots', 'number_of_headshots',  'number_of_kills',  'number_of_jumps', 'walking_time', 'running_time', 'crouch_time', 'is_aggressive'])
	for i in range(2):
		for file_path in[os.path.join(paths[i], fl) for fl in os.listdir(paths[i])]:
			number_of_jumps = 0
			number_of_shots = 0
			number_of_headshots = 0
			number_of_succ_shots = 0
			number_of_kills = 0
			#game_play_time = 
			walking_time = 0
			running_time = 0
			crouch_time = 0
			idle_time = 0
			tot_num_actions = 0

			is_crouching = False
			is_running = False
			is_walking = False
			walking_time_start_temp = dt.datetime.strptime("2:00:04.739", '%H:%M:%S.%f') - dt.datetime.strptime("2:00:04.739", '%H:%M:%S.%f') 
			running_time_start_temp = dt.datetime.strptime("2:00:04.739", '%H:%M:%S.%f') - dt.datetime.strptime("2:00:04.739", '%H:%M:%S.%f') 
			crouching_time_start_temp = dt.datetime.strptime("2:00:04.739", '%H:%M:%S.%f') - dt.datetime.strptime("2:00:04.739", '%H:%M:%S.%f')

			f = open(file_path, "r")#Boar_Hit, Boar_Dead , Soldier_Hit , Soldier_Dead
			for x in f:
				array = x.split()
				tot_num_actions += 1
				# print(array[0])
				if array[0] == "Shooting":
					number_of_shots += 1
				elif array[0] == "Zombie_Hit" or array[0] == "Soldier_Hit" or array[0] == "Boar_Hit":
					number_of_succ_shots += 1
				elif array[0] == "Zombie_HeadShot":
					number_of_headshots += 1
				elif array[0] == "Zombie_dead" or array[0] == "Boar_Dead" or array[0] == "Soldier_dead":
					number_of_kills += 1
				elif array[0] == "Jump":
					number_of_jumps += 1
				elif array[0] == ("Walking_Forward" or "Walking_Backword" or "Walking_Left" or "Walking_Right") and not is_running and not is_crouching and not is_walking:
					is_walking = True
					try:
						walking_time_start_temp = dt.datetime.strptime(get_time(array[1]), '%H:%M:%S.%f')
					except:
						continue
				elif array[0] == ("Walking_Forward_END" or "Walking_Backword_END" or "Walking_Left_END" or "Walking_Right_END") and not is_running and not is_crouching and is_walking:
					is_walking = False
					try:
						walking_time += (dt.datetime.strptime(get_time(array[1]), '%H:%M:%S.%f') - walking_time_start_temp).total_seconds()
					except:
						continue
				elif array[0] == ("Running_Forward" or "Running_Left" or "Running_Right" or "Running_Backword") and not is_running:
					is_running = True
					try:
						running_time_start_temp = dt.datetime.strptime(array[1], '%H:%M:%S.%f')
					except:
						continue
				elif array[0] == ("Running_Forward_END" or "Running_Left_END" or "Running_Right_END" or "Running_Backword_END") and is_running:
					is_running = False
					try:
						running_time += (dt.datetime.strptime(get_time(array[1]), '%H:%M:%S.%f') - running_time_start_temp).total_seconds()
					except:
						continue
				elif array[0] == "Crouch_started" and not is_crouching:
					is_crouching = True
					try:
						crouching_time_start_temp = dt.datetime.strptime(get_time(array[1]), '%H:%M:%S.%f')
					except:
						continue
				elif array[0] == "Crouch_end" and is_crouching:
					is_crouching = False
					try:
						crouch_time += (dt.datetime.strptime(get_time(array[1]), '%H:%M:%S.%f') - crouching_time_start_temp).total_seconds()
					except:
						continue
			f.close()

			# print(tot_num_actions, number_of_shots, number_of_succ_shots, number_of_headshots, number_of_jumps, walking_time, running_time, crouch_time)
			data_set_writter.writerow([file_path, tot_num_actions, number_of_shots, number_of_succ_shots, number_of_headshots, number_of_kills, number_of_jumps, walking_time, running_time, crouch_time, i])