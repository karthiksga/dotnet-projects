import { Money } from './types';
import { MutationTree } from 'vuex';
import { MachineState } from "./types";
import { INSERT_MONEY, GET_MONEY_IN_MACHINE, MONEY_INSERTED } from './constants';

export const mutations: MutationTree<MachineState> = {
    [MONEY_INSERTED](state, money: Money) {
        state.moneyInMachine = money;
    }
}