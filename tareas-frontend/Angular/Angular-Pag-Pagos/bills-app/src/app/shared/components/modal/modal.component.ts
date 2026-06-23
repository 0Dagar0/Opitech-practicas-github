import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
    selector: 'app-modal',
    standalone: true,
    imports: [CommonModule],
    template: `
        <div class="modal-overlay" *ngIf="isOpen" (click)="close()">
            <div class="modal-container" (click)="$event.stopPropagation()">
                <div class="modal-header">
                    <h3>{{ title }}</h3>
                    <button class="modal-close" (click)="close()">✕</button>
                </div>
                <div class="modal-body">
                    <ng-content></ng-content>
                </div>
                <div class="modal-footer" *ngIf="showFooter">
                    <button class="btn-cancel" (click)="close()">Cancelar</button>
                    <button class="btn-confirm" (click)="confirm()">Confirmar</button>
                </div>
            </div>
        </div>
    `,
    styles: [`
        .modal-overlay {
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background: rgba(0, 0, 0, 0.5);
            display: flex;
            justify-content: center;
            align-items: center;
            z-index: 1000;
        }
        .modal-container {
            background: white;
            border-radius: 12px;
            width: 90%;
            max-width: 500px;
            max-height: 90vh;
            overflow-y: auto;
            box-shadow: 0 20px 25px -5px rgba(0, 0, 0, 0.2);
        }
        .modal-header {
            display: flex;
            justify-content: space-between;
            align-items: center;
            padding: 1rem 1.5rem;
            border-bottom: 1px solid #e2e8f0;
        }
        .modal-header h3 {
            margin: 0;
            color: #1e293b;
        }
        .modal-close {
            background: none;
            border: none;
            font-size: 1.5rem;
            cursor: pointer;
            color: #94a3b8;
        }
        .modal-close:hover {
            color: #ef4444;
        }
        .modal-body {
            padding: 1.5rem;
        }
        .modal-footer {
            display: flex;
            justify-content: flex-end;
            gap: 0.75rem;
            padding: 1rem 1.5rem;
            border-top: 1px solid #e2e8f0;
        }
        .btn-cancel {
            background: #f1f5f9;
            color: #475569;
            border: none;
            padding: 0.5rem 1rem;
            border-radius: 8px;
            cursor: pointer;
        }
        .btn-confirm {
            background: #10b981;
            color: white;
            border: none;
            padding: 0.5rem 1rem;
            border-radius: 8px;
            cursor: pointer;
        }
        .btn-confirm:hover {
            background: #059669;
        }
    `]
})
export class ModalComponent {
    @Input() isOpen: boolean = false;
    @Input() title: string = '';
    @Input() showFooter: boolean = true;
    @Output() onClose = new EventEmitter<void>();
    @Output() onConfirm = new EventEmitter<void>();

    close(): void {
        this.onClose.emit();
    }

    confirm(): void {
        this.onConfirm.emit();
    }
}