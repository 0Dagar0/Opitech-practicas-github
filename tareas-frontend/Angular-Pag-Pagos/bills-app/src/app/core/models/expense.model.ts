export interface Expense {
    id: number;
    description: string;
    amount: number;
    date: Date;
    category: ExpenseCategory;
}

export enum ExpenseCategory {
    Food,
    Transportation,
    Entertainment,
    Utilities,
    Healthcare,
    Other
}