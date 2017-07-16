/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { UserCartComponent } from './user-cart.component';

describe('UserCartComponent', () => {
  let component: UserCartComponent;
  let fixture: ComponentFixture<UserCartComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UserCartComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UserCartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
