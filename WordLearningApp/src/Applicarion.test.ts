import { addNewSet, Application, DeleteAppSet } from "./Application";
import { CardSet } from "./CardSet";

describe(`Application`, () => {
    describe(`addNewSet`, () => {
        it('should add a new set to the application', () => {
            const initialApp: Application = { cardsSet: [] };
            const updatedApp: Application = addNewSet(initialApp, 'New Set');
            expect(updatedApp.cardsSet.length).toBe(1);
            expect(updatedApp.cardsSet[0].cards).toEqual([]);
        });
    });
    describe(`DeleteAppSet`, () => {
        it('should delete a set from the application', () => {
          const set1: CardSet = { id: '1', name: 'Set 1', cards: [] };
          const set2: CardSet = { id: '2', name: 'Set 2', cards: [] };
          const initialApp: Application = { cardsSet: [set1, set2] };
          const updatedApp = DeleteAppSet(initialApp, '1');
          expect(updatedApp.cardsSet[0].name).toBe('Set 2');
          expect(updatedApp.cardsSet.length).toBe(1);
        });
    });
}); 