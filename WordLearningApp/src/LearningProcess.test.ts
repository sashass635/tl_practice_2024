import { CardSet } from "./CardSet";
import { GetLearnedCards, MarkCardAsLearned, StartLearningProcess } from "./LearningProcess";

describe(`LearningProcess`, () => {
    describe('StartLearningProcess', () => {
        it('should initialize the learning process with all cards unlearned', () => {
          const cardSet: CardSet = { id: '1', name: 'Test Set', cards: [
            { id: '1', word: 'Hello', translation: 'Здравствуйте' },
            { id: '2', word: 'Please', translation: 'Пожалуйста' }
          ]};
          const process = StartLearningProcess(cardSet);
          expect(process.learnedCards).toEqual([]);
          expect(process.unlearnedCards).toEqual(cardSet.cards);
        });
    });
    describe('MarkCardAsLearned', () => {
        it('should move a card from unlearned to learned', () => {
            const cardSet: CardSet = { id: '1', name: 'Test Set', cards: [
                { id: '1', word: 'Hello', translation: 'Здравствуйте' },
                { id: '2', word: 'Please', translation: 'Пожалуйста' }
            ]};
            let process = StartLearningProcess(cardSet);
            process = MarkCardAsLearned(process, '1');
            expect(process.learnedCards).toEqual([{ id: '1', word: 'Hello', translation: 'Здравствуйте' }]);
            expect(process.unlearnedCards).toEqual([{ id: '2', word: 'Please', translation: 'Пожалуйста' }]);
        });
    });
    describe('GetLearnedCards', () => {
        it('should return all learned cards', () => {
          const cardSet: CardSet = { id: '1', name: 'Test Set', cards: [
            { id: '1', word: 'Hello', translation: 'Здравствуйте' },
            { id: '2', word: 'Please', translation: 'Пожалуйста' }
          ]};
          let process = StartLearningProcess(cardSet);
          process = MarkCardAsLearned(process, '1');
          const learnedCards = GetLearnedCards(process);
          expect(learnedCards).toEqual([{ id: '1', word: 'Hello', translation: 'Здравствуйте' }]);
        });
    });
});