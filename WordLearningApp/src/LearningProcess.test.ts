import { CardSet } from "./CardSet";
import { UpdateCardStatusLearning, StartLearningProcess } from "./LearningProcess";

describe(`LearningProcess`, () => {
    describe('StartLearningProcess', () => {
        it('should initialize the learning process with all cards unlearned', () => {
          const cardSet: CardSet = { id: '1', name: 'Test Set', cards: [
            { id: '1', word: 'Hello', translation: 'Здравствуйте' },
            { id: '2', word: 'Please', translation: 'Пожалуйста' }
          ]};
          const process = StartLearningProcess(cardSet);
          expect(process.unlearnedCards).toEqual(cardSet.cards);
        });
    });
    describe('MarkCardAsLearned', () => {
        it('should move a card from unlearned', () => {
            const cardSet: CardSet = { id: '1', name: 'Test Set', cards: [
                { id: '1', word: 'Hello', translation: 'Здравствуйте' },
                { id: '2', word: 'Please', translation: 'Пожалуйста' }
            ]};
            let process = StartLearningProcess(cardSet);
            process = UpdateCardStatusLearning(process, '1', true);
            expect(process.unlearnedCards).toEqual([{ id: '2', word: 'Please', translation: 'Пожалуйста' }]);
        });
        it('should move a card from unlearned to the end of the list', () => {
          const cardSet: CardSet = { id: '1', name: 'Test Set', cards: [
              { id: '1', word: 'Hello', translation: 'Здравствуйте' },
              { id: '2', word: 'Please', translation: 'Пожалуйста' }
          ]};
          let process = StartLearningProcess(cardSet);
          process = UpdateCardStatusLearning(process, '1', false);
          expect(process.unlearnedCards).toEqual([
              { id: '2', word: 'Please', translation: 'Пожалуйста' },
              { id: '1', word: 'Hello', translation: 'Здравствуйте' }
          ]);
        });
    });
});