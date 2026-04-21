export interface Expense {
    id: number;
    description: string;
    amount: number;
    date: Date;
    category: ExpenseCategory;
}

export enum ExpenseCategory {
    Comida,
    Transporte,
    Vivienda,
    Entretenimiento,
    Salud,
    Otros
}