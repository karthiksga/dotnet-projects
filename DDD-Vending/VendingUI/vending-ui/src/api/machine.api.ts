import { Money } from '../store/profile/types';
import axios from 'axios';

const instance = axios.create( {
    baseURL: process.env.VUE_APP_API,
});

export class MachineApi {
    public async insertMoney (money:Money) {
        return await this.post(money);
    };

    async post(data: any) {
        return await instance.post('/',data);
    };
}
