import { GetterTree } from 'vuex';
import { MachineState, Money } from './types';
import { RootState } from '../types';


export const getters: GetterTree<MachineState,RootState> = {
    moneyInMachine (state) : Money | undefined {
        return state.moneyInMachine;
    }
}