/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { ItemService } from './item.service';

describe('ItemService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ItemService]
    });
  });

  it('should ...', inject([ItemService], (service: ItemService) => {
    expect(service).toBeTruthy();
  }));
});
