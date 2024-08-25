import { CreateCard, UpdateCard, Card, DeleteCard } from "./Card";
import { CardSet } from "./CardSet";

describe(`Card`, () => {
    describe('CreateCard', () => {
        it('should create a card and add it to the card set', () => {
            const word = 'Hello';
            const translation = 'Здравствуйте';
            const set: CardSet = {id: '1', name: 'Test Set', cards: [] };
            const updatedSetCard: CardSet = CreateCard(word, translation, set);
            expect(updatedSetCard.cards).toHaveLength(1);
            expect(updatedSetCard.cards[0].word).toBe(word);
            expect(updatedSetCard.cards[0].translation).toBe(translation);
        });
    });
    describe('UpdateCard', () => {
        it('should update the translation of a card', () => {
            const set: CardSet  = {id: '1', name: 'Test Set', cards: [{ id: '1', word: 'fine', translation: 'хорошо' }]};
            const updatedSet: CardSet = UpdateCard(set, '1', undefined, 'штраф');           
            expect(updatedSet.cards).toEqual([{ id: '1', word: 'fine', translation: 'штраф' }]); 
        });
        it('should update the word of a card', () => {
            const set: CardSet  = {id: '1', name: 'Test Set', cards: [{ id: '2', word: 'blue', translation: 'печальный' }]};
            const updatedSet: CardSet = UpdateCard(set, '2', 'sad', undefined);
            expect(updatedSet.cards).toEqual([{ id: '2', word: 'sad', translation: 'печальный' }]);
        });
    });
    describe('DeleteCard', () => {
        it('should delete card from the list', () => {
            const set: CardSet  = {id: '1', name: 'Test Set', cards: [{ id: '1', word: 'fine', translation: 'хорошо' }]};
            const deletedCard: CardSet = DeleteCard(set, '1');
            expect(deletedCard.cards.find(card => card.id === '1')).toBeUndefined();
        });
    });
});