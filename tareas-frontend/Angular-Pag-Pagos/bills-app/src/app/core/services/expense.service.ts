import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Expense } from '../models/expense.model';

@Injectable({
    providedIn: 'root',
})
export class ExpenseService {

    private apiUrl = 'https://localhost:7072/expenses';

    constructor(private http: HttpClient) {}

    // 🔹 GET
    getExpenses() {
        return this.http.get<Expense[]>(this.apiUrl);
    }

    // 🔹 POST
    addExpense(expense: Omit<Expense, 'id'>) {
        return this.http.post(`${this.apiUrl}/add`, expense);
    }

    // 🔹 PUT
    updateExpense(expense: Expense) {
        return this.http.put(`${this.apiUrl}/update/${expense.id}`, expense);
    }

    // 🔹 DELETE
    deleteExpense(id: number) {
        return this.http.delete(`${this.apiUrl}/delete/${id}`);
    }
}