import { CardSet } from "./CardSet";
import { StartLearningProcess, MarkCardAsLearned, MoveCardToBottom } from "./LearningProcess";

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
        describe('MarkCardAsLearned', () => {
            it('should move a card from unlearned to learned', () => {
                const cardSet: CardSet = { id: '1', name: 'Test Set', cards: [
                    { id: '1', word: 'Hello', translation: 'Здравствуйте' },
                    { id: '2', word: 'Please', translation: 'Пожалуйста' }
                ]};
                let process = StartLearningProcess(cardSet);
                process = MarkCardAsLearned(process, '1');
                expect(process.unlearnedCards).toEqual([{ id: '2', word: 'Please', translation: 'Пожалуйста' }]);
            });
        });
    });
    describe('MoveCardToBottom', () => {
        it('should move the card to the end of unlearned сards', () => {
            const cardSet: CardSet = { id: '1', name: 'Test Set', cards: [
                { id: '1', word: 'Hello', translation: 'Здравствуйте' },
                { id: '2', word: 'Please', translation: 'Пожалуйста' },
                { id: '3', word: 'Fine', translation: 'Штраф' }
            ]};
            let process = StartLearningProcess(cardSet);
           
            process = MoveCardToBottom(process, '1');
            
            expect(process.unlearnedCards).toEqual([
                { id: '2', word: 'Please', translation: 'Пожалуйста' },
                { id: '3', word: 'Fine', translation: 'Штраф' },
                { id: '1', word: 'Hello', translation: 'Здравствуйте' } 
            ]);
        });
        it('should not change the process if card id does not exist', () => {
            const cardSet: CardSet = { id: '1', name: 'Test Set', cards: [
                { id: '1', word: 'Hello', translation: 'Здравствуйте' },
                { id: '2', word: 'Please', translation: 'Пожалуйста' }
            ]};
            let process = StartLearningProcess(cardSet);
    
            process = MoveCardToBottom(process, '3');
    
            expect(process.unlearnedCards).toEqual([
                { id: '1', word: 'Hello', translation: 'Здравствуйте' },
                { id: '2', word: 'Please', translation: 'Пожалуйста' }
            ]);
        });
    });
});