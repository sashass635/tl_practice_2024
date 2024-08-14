import { СreateCard, UpdateCard, Card, DeleteCard } from "./Card";

describe(`Card`, () => {
    describe(`СreateCard`, () => {
      it(`should create a card `, () => {
        const word = 'Hello';
        const translation = 'Здравствуйте';
        const card: Card = СreateCard(word, translation);
        expect(card.word).toBe(word);
        expect(card.translation).toBe(translation);
        });
    });
    describe('UpdateCard', () => {
        it('should update the translation of a card', () => {
            const card: Card[] = [{ id: '1', word: 'fine', translation: 'хорошо' }];
            const updatedCard: Card[] = UpdateCard(card, '1', undefined, 'штраф');
            expect(updatedCard).toEqual([{ id: '1', word: 'fine', translation: 'штраф' }]); 
        });
        it('should update the word of a card', () => {
            const card: Card[] = [{ id: '2', word: 'blue', translation: 'печальный' }];
            const updatedCard: Card[] = UpdateCard(card, '2', 'sad', undefined);
            expect(updatedCard).toEqual([{ id: '2', word: 'sad', translation: 'печальный' }]);
        });
    });
    describe('DeleteCard', () => {
        it('should delete card from the list', () => {
            const card: Card[] = [{ id: '1', word: 'fine', translation: 'хорошо' }];
            const deletedCard: Card[] = DeleteCard(card, '1');
            expect(deletedCard.find(card => card.id === '1')).toBeUndefined();
        });
    });
});