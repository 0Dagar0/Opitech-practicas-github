import { HttpClient } from '@angular/common/http';
import { Injectable, signal } from '@angular/core';
import { Expense } from '../models/expense.model';
import { enviroment } from '../../../enviroments/enviroment';



@Injectable({
    providedIn: 'root',
})
export class ExpenseService {

    private expensesSignal = signal<Expense[]>([]);
    public expenses = this.expensesSignal.asReadonly();

    private totalSignal = signal<number>(0);
    public total = this.totalSignal.asReadonly();

    private apiUrl = 'https://localhost:7072/expenses';

    constructor(private http: HttpClient) { }

    loadExpenses() {
        this.getExpenses().subscribe({
            next: (data) => {
                this.expensesSignal.set(data);
                const total = data.reduce((acc, curr) => acc + curr.amount, 0);
                this.totalSignal.set(total);
            },
            error: (err) => {
                console.error('Error al cargar gastos:', err);
            }
        });
    }

    // 🔹 GET
    getExpenses() {
        return this.http.get<Expense[]>(this.apiUrl);
    }

    // 🔹 POST
    addExpense(expense: Omit<Expense, 'id'>) {
        this.http.post<Expense>(`${this.apiUrl}/add`, expense).subscribe({
            next: (newExpense) => {
                this.expensesSignal.update(lista => [...lista, newExpense]);
                this.totalSignal.update(total => total + newExpense.amount);
            },
            error: (err) => {
                console.error('Error al agregar gasto:', err);
                alert('Error al guardar el gasto');
            }
        });
    }

    // 🔹 PUT
    updateExpense(expense: Expense) {
        const categoryNumber = this.getCategoryNumber(expense.category);

        const expenseToSend = {
            id: expense.id,
            description: expense.description,
            amount: expense.amount,
            date: expense.date,
            category: categoryNumber 
        };

        console.log('Enviando a la API:', expenseToSend);

        this.http.put(`${this.apiUrl}/update/${expense.id}`, expenseToSend).subscribe({
            next: () => {
                const gastoOriginal = this.expensesSignal().find(g => g.id === expense.id);
                if (!gastoOriginal) return;

                this.expensesSignal.update(lista =>
                    lista.map(g => g.id === expense.id ? expense : g)
                );
                const diferencia = expense.amount - gastoOriginal.amount;
                this.totalSignal.update(total => total + diferencia);
            },
            error: (err) => {
                console.error('Error al actualizar gasto:', err);
                alert('Error al actualizar el gasto');
            }
        });
    }

    private getCategoryNumber(category: string | number): number {
        const categoriesMap: { [key: string]: number } = {
            'Food': 0,
            'Transportation': 1,
            'Entertainment': 2,
            'Utilities': 3,
            'Healthcare': 4,
            'Other': 5
        };

        if (typeof category === 'number') return category;
        return categoriesMap[category] ?? 5; 
    }

    // 🔹 DELETE
    deleteExpense(id: number) {
        const gastoAEliminar = this.expensesSignal().find(gasto => gasto.id === id);

        if (!gastoAEliminar) {
            console.error('No se encontró el gasto con id:', id);
            return;
        }

        this.http.delete(`${this.apiUrl}/delete/${id}`).subscribe({
            next: () => {
                this.expensesSignal.update(lista => lista.filter(gasto => gasto.id !== id));
                this.totalSignal.update(total => total - gastoAEliminar.amount);
            },
            error: (err) => {
                console.error('Error al eliminar gasto:', err);
                alert('Error al eliminar el gasto');
            }
        });
    }


}