export interface Money {
    oneCentCount: number,
    tenCentCount: number,
    twentyFiveCentCount: number,
    oneDollarCount: number,
    tenDollarCount: number,
    twentyDollarCount: number,
}
export interface MachineState {
    moneyInTransaction?: Money,
    moneyInMachine?: Money,
    error: boolean,
}