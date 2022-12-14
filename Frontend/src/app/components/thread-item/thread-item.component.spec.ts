import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ThreadItemComponent } from './thread-item.component';

describe('ThreadItemComponent', () => {
  let component: ThreadItemComponent;
  let fixture: ComponentFixture<ThreadItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ThreadItemComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ThreadItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
