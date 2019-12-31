import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AgendaCadastroComponent } from './agenda-cadastro.component';

describe('AgendaComponent', () => {
  let component: AgendaCadastroComponent;
  let fixture: ComponentFixture<AgendaCadastroComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AgendaCadastroComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AgendaCadastroComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
