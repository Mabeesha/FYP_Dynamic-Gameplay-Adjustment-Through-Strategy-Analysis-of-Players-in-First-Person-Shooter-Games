import numpy as np
import skfuzzy as fuzz
from skfuzzy import control as ctrl
import matplotlib.pyplot as plt
import pickle

# input to the python code
avg_running_time = 0.5
avg_number_of_jumps = 0.5
avg_Crouch_time_plus_walking_time = 0.5

# experiment parameters
# function types: triangular = 0 , sigmoid = 1
gun_damage_pc_funtype =  0
speed_npc_funtype =  1
gun_accuracy_npc_funtype =  0
gun_damage_npc_funtype = 0
health_dec_factor_funtype =  1
ammo_funtype =  1
tot_enemy_funtype =  0
tot_enemy_1_location_funtype =  0

# minimum values
gun_damage_pc_min = 50
speed_npc_min = 50
gun_accuracy_npc_min = 50
gun_damage_npc_min = 50
health_dec_factor_min = 40 
ammo_min = 50
tot_enemy_min = 10
tot_enemy_1_location_min = 10

# load the model from disk
filename = 'finalized_model.model'
loaded_model = pickle.load(open(filename, 'rb'))
# result = loaded_model.score(X_test, Y_test)
# print(result)

#output of ML model
out_ml_model = loaded_model.predict_proba([[avg_running_time, avg_number_of_jumps, avg_Crouch_time_plus_walking_time]])[0][1]
out_ml_model = out_ml_model * 100
print("is_aggresive : {}%".format(out_ml_model))

# Antecedent
is_aggresive = ctrl.Antecedent(np.arange(0, 101, 1), 'is_aggresive')

# Consequent
gun_damage_pc = ctrl.Consequent(np.arange(0, 101, 1), 'gun_damage_pc')
speed_npc = ctrl.Consequent(np.arange(0, 101, 1), 'speed_npc')
gun_accuracy_npc = ctrl.Consequent(np.arange(0, 101, 1), 'gun_accuracy_npc')
gun_damage_npc = ctrl.Consequent(np.arange(0, 101, 1), 'gun_damage_npc')
health_dec_factor = ctrl.Consequent(np.arange(0, 101, 1), 'health_dec_factor')
ammo = ctrl.Consequent(np.arange(0, 101, 1), 'ammo')
tot_enemy = ctrl.Consequent(np.arange(0, 101, 1), 'tot_enemy')
tot_enemy_1_location = ctrl.Consequent(np.arange(0, 101, 1), 'tot_enemy_1_location')

# membershio function of Antecedent
is_aggresive['passive'] = fuzz.trimf(is_aggresive.universe, [0, 0, 100])
is_aggresive['aggresive'] = fuzz.trimf(is_aggresive.universe, [0, 100, 100])

# membership functions of Consequent
if gun_damage_pc_funtype == 0:
	gun_damage_pc['low'] = fuzz.trimf(gun_damage_pc.universe, [gun_damage_pc_min, gun_damage_pc_min, 100])
	gun_damage_pc['high'] = fuzz.trimf(gun_damage_pc.universe, [gun_damage_pc_min, 100, 100])
elif gun_damage_pc_funtype == 1:
	c = (-2.0 * np.log(99)) / (gun_damage_pc_min-100)
	gun_damage_pc['low'] = fuzz.sigmf(gun_damage_pc.universe, (100+gun_damage_pc_min)/2.0, -1.0*c)
	gun_damage_pc['high'] = fuzz.sigmf(gun_damage_pc.universe, (100+gun_damage_pc_min)/2.0, c)

if speed_npc_funtype == 0:
	speed_npc['low'] = fuzz.trimf(speed_npc.universe, [speed_npc_min, speed_npc_min, 100])
	speed_npc['high'] = fuzz.trimf(speed_npc.universe, [speed_npc_min, 100, 100])
elif speed_npc_funtype == 1:
	c = (-2.0 * np.log(99)) / (gun_damage_pc_min-100)
	speed_npc['low'] = fuzz.sigmf(speed_npc.universe, (100+speed_npc_min)/2.0, -1.0*c)
	speed_npc['high'] = fuzz.sigmf(speed_npc.universe, (100+speed_npc_min)/2.0, c)

if gun_accuracy_npc_funtype == 0:
	gun_accuracy_npc['low'] = fuzz.trimf(gun_accuracy_npc.universe, [gun_accuracy_npc_min, gun_accuracy_npc_min, 100])
	gun_accuracy_npc['high'] = fuzz.trimf(gun_accuracy_npc.universe, [gun_accuracy_npc_min, 100, 100])
elif gun_accuracy_npc_funtype == 1:
	c = (-2.0 * np.log(99)) / (gun_damage_pc_min-100)
	gun_accuracy_npc['low'] = fuzz.sigmf(gun_accuracy_npc.universe, (100+gun_accuracy_npc_min)/2.0, -1.0*c)
	gun_accuracy_npc['high'] = fuzz.sigmf(gun_accuracy_npc.universe, (100+gun_accuracy_npc_min)/2.0, c)

if gun_damage_npc_funtype == 0:
	gun_damage_npc['low'] = fuzz.trimf(gun_damage_npc.universe, [gun_damage_npc_min, gun_damage_npc_min, 100])
	gun_damage_npc['high'] = fuzz.trimf(gun_damage_npc.universe, [gun_damage_npc_min, 100, 100])
elif gun_damage_npc_funtype == 1:
	c = (-2.0 * np.log(99)) / (gun_damage_pc_min-100)
	gun_damage_npc['low'] = fuzz.sigmf(gun_damage_npc.universe, (100+gun_damage_npc_min)/2.0, -1.0*c)
	gun_damage_npc['high'] = fuzz.sigmf(gun_damage_npc.universe, (100+gun_damage_npc_min)/2.0, c)

if health_dec_factor_funtype == 0:
	health_dec_factor['low'] = fuzz.trimf(health_dec_factor.universe, [health_dec_factor_min, health_dec_factor_min, 100])
	health_dec_factor['high'] = fuzz.trimf(health_dec_factor.universe, [health_dec_factor_min, 100, 100])
elif health_dec_factor_funtype == 1:
	c = (-2.0 * np.log(99)) / (gun_damage_pc_min-100)
	health_dec_factor['low'] = fuzz.sigmf(health_dec_factor.universe, (100+health_dec_factor_min)/2.0, -1.0*c)
	health_dec_factor['high'] = fuzz.sigmf(health_dec_factor.universe, (100+health_dec_factor_min)/2.0, c)

if ammo_funtype == 0:
	ammo['low'] = fuzz.trimf(ammo.universe, [ammo_min, ammo_min, 100])
	ammo['high'] = fuzz.trimf(ammo.universe, [ammo_min, 100, 100])
elif ammo_funtype == 1:
	c = (-2.0 * np.log(99)) / (gun_damage_pc_min-100)
	ammo['low'] = fuzz.sigmf(ammo.universe, (100+ammo_min)/2.0, -1.0*c)
	ammo['high'] = fuzz.sigmf(ammo.universe, (100+ammo_min)/2.0, c)

if tot_enemy_funtype == 0:
	tot_enemy['low'] = fuzz.trimf(tot_enemy.universe, [tot_enemy_min, tot_enemy_min, 100])
	tot_enemy['high'] = fuzz.trimf(tot_enemy.universe, [tot_enemy_min, 100, 100])
elif tot_enemy_funtype == 1:
	c = (-2.0 * np.log(99)) / (gun_damage_pc_min-100)
	tot_enemy['low'] = fuzz.sigmf(tot_enemy.universe, (100+tot_enemy_min)/2.0, -1.0*c)
	tot_enemy['high'] = fuzz.sigmf(tot_enemy.universe, (100+tot_enemy_min)/2.0, c)

if tot_enemy_1_location_funtype == 0:
	tot_enemy_1_location['low'] = fuzz.trimf(tot_enemy_1_location.universe, [tot_enemy_1_location_min, tot_enemy_1_location_min, 100])
	tot_enemy_1_location['high'] = fuzz.trimf(tot_enemy_1_location.universe, [tot_enemy_1_location_min, 100, 100])
elif tot_enemy_1_location_funtype == 1:
	c = (-2.0 * np.log(99)) / (gun_damage_pc_min-100)
	tot_enemy_1_location['low'] = fuzz.sigmf(tot_enemy_1_location.universe, (100+tot_enemy_1_location_min)/2.0, -1.0*c)
	tot_enemy_1_location['high'] = fuzz.sigmf(tot_enemy_1_location.universe, (100+tot_enemy_1_location_min)/2.0, c)

# plot membership functions
is_aggresive.view()
gun_damage_pc.view()
speed_npc.view()
gun_accuracy_npc.view()
gun_damage_npc.view()
health_dec_factor.view()
ammo.view() 
tot_enemy.view()
tot_enemy_1_location.view()

# rules
rule1_gun_damage_pc = ctrl.Rule(is_aggresive['passive'], gun_damage_pc['low'])
rule2_gun_damage_pc = ctrl.Rule(is_aggresive['aggresive'], gun_damage_pc['high'])

rule1_speed_npc = ctrl.Rule(is_aggresive['passive'], speed_npc['low'])
rule2_speed_npc = ctrl.Rule(is_aggresive['aggresive'], speed_npc['high'])

rule1_gun_accuracy_npc = ctrl.Rule(is_aggresive['passive'], gun_accuracy_npc['high'])
rule2_gun_accuracy_npc = ctrl.Rule(is_aggresive['aggresive'], gun_accuracy_npc['low'])

rule1_gun_damage_npc = ctrl.Rule(is_aggresive['passive'], gun_damage_npc['low'])
rule2_gun_damage_npc = ctrl.Rule(is_aggresive['aggresive'], gun_damage_npc['high'])

rule1_health_dec_factor = ctrl.Rule(is_aggresive['passive'], health_dec_factor['high'])
rule2_health_dec_factor = ctrl.Rule(is_aggresive['aggresive'], health_dec_factor['low'])

rule1_ammo = ctrl.Rule(is_aggresive['passive'], ammo['low'])
rule2_ammo = ctrl.Rule(is_aggresive['aggresive'], ammo['high'])

rule1_tot_enemy = ctrl.Rule(is_aggresive['passive'], tot_enemy['high'])
rule2_tot_enemy = ctrl.Rule(is_aggresive['aggresive'], tot_enemy['low'])

rule1_tot_enemy_1_location = ctrl.Rule(is_aggresive['passive'], tot_enemy_1_location['high'])
rule2_tot_enemy_1_location = ctrl.Rule(is_aggresive['aggresive'], tot_enemy_1_location['low'])

# control systems
gun_damage_pc_ctrl = ctrl.ControlSystem([rule1_gun_damage_pc, rule2_gun_damage_pc])
speed_npc_ctrl = ctrl.ControlSystem([rule1_speed_npc, rule2_speed_npc])
gun_accuracy_npc_ctrl = ctrl.ControlSystem([rule1_gun_accuracy_npc, rule2_gun_accuracy_npc])
gun_damage_npc_ctrl = ctrl.ControlSystem([rule1_gun_damage_npc, rule2_gun_damage_npc])
health_dec_factor_ctrl = ctrl.ControlSystem([rule1_health_dec_factor, rule2_health_dec_factor])
ammo_ctrl = ctrl.ControlSystem([rule1_ammo, rule2_ammo])
tot_enemy_ctrl = ctrl.ControlSystem([rule1_tot_enemy, rule2_tot_enemy])
tot_enemy_1_location_ctrl = ctrl.ControlSystem([rule1_tot_enemy_1_location, rule2_tot_enemy_1_location])
# tipping_ctrl = ctrl.ControlSystem([rule1, rule2, rule3])

# control simulation
gun_damage_pc_ctrling = ctrl.ControlSystemSimulation(gun_damage_pc_ctrl)
speed_npc_ctrling = ctrl.ControlSystemSimulation(speed_npc_ctrl)
gun_accuracy_npc_ctrling = ctrl.ControlSystemSimulation(gun_accuracy_npc_ctrl)
gun_damage_npc_ctrling = ctrl.ControlSystemSimulation(gun_damage_npc_ctrl)
health_dec_factor_ctrling = ctrl.ControlSystemSimulation(health_dec_factor_ctrl)
ammo_ctrling = ctrl.ControlSystemSimulation(ammo_ctrl)
tot_enemy_ctrling = ctrl.ControlSystemSimulation(tot_enemy_ctrl)
tot_enemy_1_location_ctrling = ctrl.ControlSystemSimulation(tot_enemy_1_location_ctrl)
# tipping = ctrl.ControlSystemSimulation(tipping_ctrl)

gun_damage_pc_ctrling.input['is_aggresive'] = out_ml_model
speed_npc_ctrling.input['is_aggresive'] = out_ml_model
gun_accuracy_npc_ctrling.input['is_aggresive'] = out_ml_model
gun_damage_npc_ctrling.input['is_aggresive'] = out_ml_model
health_dec_factor_ctrling.input['is_aggresive'] = out_ml_model
ammo_ctrling.input['is_aggresive'] = out_ml_model 
tot_enemy_ctrling.input['is_aggresive'] = out_ml_model
tot_enemy_1_location_ctrling.input['is_aggresive'] = out_ml_model
# tipping.input['quality'] = 6.5

gun_damage_pc_ctrling.compute()
speed_npc_ctrling.compute()
gun_accuracy_npc_ctrling.compute()
gun_damage_npc_ctrling.compute()
health_dec_factor_ctrling.compute()
ammo_ctrling.compute() 
tot_enemy_ctrling.compute()
tot_enemy_1_location_ctrling.compute()
# tipping.compute()

print("Gun damage PC:  " + str(gun_damage_pc_ctrling.output['gun_damage_pc']))
print("Speed NPC:  " + str(speed_npc_ctrling.output['speed_npc']))
print("Gun accuracy NPC:  " + str(gun_accuracy_npc_ctrling.output['gun_accuracy_npc']))
print("Gun damage NPC:  " + str(gun_damage_npc_ctrling.output['gun_damage_npc']))
print("Health decreasing factor:  " + str(health_dec_factor_ctrling.output['health_dec_factor']))
print("Ammo:  " + str(ammo_ctrling.output['ammo'])) 
print("Total enemy count:  " + str(tot_enemy_ctrling.output['tot_enemy']))
print("Max enemy count at one spawn location:  " + str(tot_enemy_1_location_ctrling.output['tot_enemy_1_location']))
# print(tipping.output['tip'])
# tip.view(sim=tipping)

plt.show()