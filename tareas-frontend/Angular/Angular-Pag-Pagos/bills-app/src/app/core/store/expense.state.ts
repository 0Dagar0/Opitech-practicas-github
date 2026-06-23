import { Expense } from '../models/expense.model';

export interface ExpenseState {
    expenses: Expense[];
    total: number;
    loading: boolean;
    error: string | null;
}

export const initialState: ExpenseState = {
    expenses: [],
    total: 0,
    loading: false,
    error: null
};