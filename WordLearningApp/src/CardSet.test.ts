import {  CardSet, createSet, deleteSet, markCardAsLearned, moveCardToBottom } from "./CardSet";

describe(`CardSet`, () => {
    describe(`CreateSet`, () => {
      it(`should create a new card set`, () => {
        const newSet: CardSet = createSet('New Set');
        expect(newSet).toEqual({ id: expect.any(String), name: 'New Set', cards: []});      
        });
    });
    describe(`DeleteSet`, () => {
        it(`should delete a card set `, () => {
          const set: CardSet[] = [{ id: '1', name: 'fine', cards: [] }];
          const deletedCardSet:  CardSet[] = deleteSet(set, '1');
          expect(deletedCardSet.find(cardSet => cardSet.id === '1')).toBeUndefined();         
        });
    });
    describe('markCardAsLearned', () => {
      it('should remove the top card from the deck', () => {
          const deck: CardSet = {
              id: '1',
              name: 'Test Set',
              cards: [
                  { id: '1', word: 'Hello', translation: 'Здравствуйте' },
                  { id: '2', word: 'Hi', translation: 'Привет' }
              ]
          };
          const updatedDeck = markCardAsLearned(deck);
          expect(updatedDeck.cards.length).toBe(1);
          expect(updatedDeck.cards[0].id).toBe('2');
      });
      it('should leave the deck empty if there was only one card', () => {
        const deck: CardSet = {
            id: '1',
            name: 'Test Set',
            cards: [{ id: '1', word: 'Hello', translation: 'Здравствуйте' }]
        };
        const updatedDeck = markCardAsLearned(deck);
        expect(updatedDeck.cards.length).toBe(0);
    });
  });
describe('moveCardToBottom', () => {
  it('should move the top card to the bottom of the deck', () => {
      const deck: CardSet = {
          id: '1',
          name: 'Test Set',
          cards: [
              { id: '1', word: 'Hello', translation: 'Здравствуйте' },
              { id: '2', word: 'Hi', translation: 'Привет' },
              { id: '3', word: 'Goodbye', translation: 'До свидания' }
          ]
      };
      const updatedDeck = moveCardToBottom(deck);
      expect(updatedDeck.cards.length).toBe(3);
      expect(updatedDeck.cards[2].id).toBe('1'); 
      expect(updatedDeck.cards[0].id).toBe('2'); 
    });

  it('should leave the deck unchanged if it only has one card', () => {
      const deck: CardSet = {
          id: '1',
          name: 'Test Set',
          cards: [{ id: '1', word: 'Hello', translation: 'Здравствуйте' }]
      };
      const updatedDeck = moveCardToBottom(deck);
      expect(updatedDeck.cards.length).toBe(1);
      expect(updatedDeck.cards[0].id).toBe('1'); 
    });
  });
});