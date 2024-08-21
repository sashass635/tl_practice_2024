import {  CardSet, CreateSet, DeleteSet } from "./CardSet";

describe(`CardSet`, () => {
    describe(`CreateSet`, () => {
      it(`should create a new card set`, () => {
        const newSet: CardSet = CreateSet('New Set');
        expect(newSet).toEqual({ id: expect.any(String), name: 'New Set', cards: []});      
        });
    });
    describe(`DeleteSet`, () => {
        it(`should delete a card set `, () => {
          const set: CardSet[] = [{ id: '1', name: 'fine', cards: [] }];
          const deletedCardSet:  CardSet[] = DeleteSet(set, '1');
          expect(deletedCardSet.find(cardSet => cardSet.id === '1')).toBeUndefined();         
        });
    });
});