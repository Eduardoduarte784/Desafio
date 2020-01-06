import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CaixaConfirmacaoComponent } from './caixa-confirmacao.component';

describe('CaixaConfirmacaoComponent', () => {
  let component: CaixaConfirmacaoComponent;
  let fixture: ComponentFixture<CaixaConfirmacaoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CaixaConfirmacaoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CaixaConfirmacaoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
