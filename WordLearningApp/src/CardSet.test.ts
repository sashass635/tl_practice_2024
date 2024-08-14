import { Card, СreateCard } from "./Card";
import { AddCardToSet, CardSet, CreateSet, DeleteSet, GetCardsInSet } from "./CardSet";

describe(`CardSet`, () => {
    describe(`CreateSet`, () => {
      it(`should create a new card set`, () => {
        const newSet: CardSet = CreateSet('New Set');
        expect(newSet).toEqual({ id: expect.any(String), name: 'New Set', cards: []});    
      });
    });
    describe(`AddCardToSet`, () => {
      it(`should create a new card set`, () => {
        const word = 'Hello';
        const translation = 'Здравствуйте';
        const set = CreateSet('Test Set');
        const card: Card = СreateCard(word, translation);
        const updatedSet : CardSet = AddCardToSet(set, card);
        expect(updatedSet.cards).toContain(card);
       });
    });
    describe(`DeleteSet`, () => {
        it(`should delete a card set `, () => {
          const set: CardSet[] = [{ id: '1', name: 'fine', cards: [] }];
          const deletedCardSet:  CardSet[] = DeleteSet(set, '1');
          expect(deletedCardSet.find(cardSet => cardSet.id === '1')).toBeUndefined();         
        });
    });
    describe(`GetCardsInSet`, () => {
        it('should return all cards in the set', () => {
          const card1: Card = { id: '1', word: 'Hello', translation: 'Здравствуйте' };
          const card2: Card = { id: '2', word: 'Please', translation: 'Пожалуйста' };
          let cardSet: CardSet = CreateSet('Test Set');
          cardSet = AddCardToSet(cardSet, card1);
          cardSet = AddCardToSet(cardSet, card2);
          const cards: Card[] = GetCardsInSet(cardSet);
          expect(cards).toContain(card1);
          expect(cards).toContain(card2);
        });
    });
});