import { Module } from 'vuex';
import { getters } from './getters';
import { actions } from './actions';
import { mutations } from './mutations';
import { RootState } from '../types';
import { MachineState } from './types';


export const state: MachineState = {
    moneyInTransaction :  undefined,
    moneyInMachine : undefined,
    error: false
};

const namespaced: boolean = true;

export const profile: Module<MachineState,RootState> = {
    namespaced,
    state,
    getters,
    actions,
    mutations
}