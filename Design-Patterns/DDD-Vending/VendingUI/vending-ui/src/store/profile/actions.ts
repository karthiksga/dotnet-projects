import { ActionTree } from 'vuex';
import { MachineState, Money } from './types';
import { RootState } from '../types';
import { MachineApi }  from '../../api/machine.api';

export const actions: ActionTree<MachineState,RootState> = {
    async insertMoney( {commit}, money:Money)  {
        return await new MachineApi().insertMoney(money);
    }
}