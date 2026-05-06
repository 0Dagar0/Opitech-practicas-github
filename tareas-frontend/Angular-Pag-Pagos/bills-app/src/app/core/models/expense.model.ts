export interface Expense {
    id: number;
    description: string;
    amount: number;
    date: Date;
    category: ExpenseCategory;
}

export interface ExpenseForm {
    description: string;
    amount: number | null;
    date: string;
    category: ExpenseCategory | null;
}

export enum ExpenseCategory {
    Food,
    Transportation,
    Entertainment,
    Utilities,
    Healthcare,
    Other
}