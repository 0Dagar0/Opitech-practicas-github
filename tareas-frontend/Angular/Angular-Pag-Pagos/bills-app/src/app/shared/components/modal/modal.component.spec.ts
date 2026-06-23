import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ModalComponent } from './modal.component';
import { vi } from 'vitest';

describe('ModalComponent', () => {
    let component: ModalComponent;
    let fixture: ComponentFixture<ModalComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            imports: [ModalComponent]
        }).compileComponents();

        fixture = TestBed.createComponent(ModalComponent);
        component = fixture.componentInstance;

        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });

    it('should emit onClose when close() is called', () => {
        const emitSpy = vi.spyOn(component.onClose, 'emit');

        component.close();

        expect(emitSpy).toHaveBeenCalled();
    });

    it('should emit onConfirm when confirm() is called', () => {
        const emitSpy = vi.spyOn(component.onConfirm, 'emit');

        component.confirm();

        expect(emitSpy).toHaveBeenCalled();
    });
});