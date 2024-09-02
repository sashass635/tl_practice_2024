import { create } from "zustand";
import { createJSONStorage, persist } from "zustand/middleware";
import { createCard, updateCard, deleteCard } from "../types/Card";
import { CardSet, createSet, deleteSet, markCardAsLearned, moveCardToBottom } from "../types/CardSet";

type StoreData = {
  application: { cardsSet: CardSet[] };
  currentSet: CardSet | null;
  actions: {
    setCurrentSet: (newSet: CardSet | null) => void;
    addCardSet: (name: string) => void;
    deleteCardSet: (id: string) => void;
    addCardToSet: (setId: string, word: string, translation: string) => void;
    updateCardInSet: (setId: string, cardId: string, updatedWord?: string, updatedTranslation?: string) => void;
    deleteCardFromSet: (setId: string, cardId: string) => void;
    markCardAsLearned: (setId: string) => void;
    moveCardToBottom: (setId: string) => void;
  };
};

export const useStore = create<StoreData>()(
  persist(
    (set, get) => ({
      application: { cardsSet: [] },
      currentSet: null,
      actions: {
        setCurrentSet: (newSet) => {
          const { application } = get();
          const currentSet = application.cardsSet.find((set) => set.id === newSet?.id);
          set({ currentSet });
        },
        addCardSet: (name) => {
          const { application } = get();
          const newSet = createSet(name);
          const updatedApp = {
            ...application,
            cardsSet: [...application.cardsSet, newSet],
          };
          set({ application: updatedApp });
        },
        deleteCardSet: (id) => {
          const { application } = get();
          const updatedApp = {
            ...application,
            cardsSet: deleteSet(application.cardsSet, id),
          };
          set({ application: updatedApp });
        },
        addCardToSet: (setId, word, translation) => {
          const { application } = get();
          const updatedSets = application.cardsSet.map((cardSet) =>
            cardSet.id === setId ? { ...cardSet, cards: [...cardSet.cards, createCard(word, translation)] } : cardSet,
          );
          set({ application: { ...application, cardsSet: updatedSets } });
        },
        updateCardInSet: (setId, cardId, updatedWord, updatedTranslation) => {
          const { application } = get();
          const updatedSets = application.cardsSet.map((cardSet) =>
            cardSet.id === setId
              ? { ...cardSet, cards: updateCard(cardSet.cards, cardId, updatedWord, updatedTranslation) }
              : cardSet,
          );
          set({ application: { ...application, cardsSet: updatedSets } });
        },
        deleteCardFromSet: (setId, cardId) => {
          const { application } = get();
          const updatedSets = application.cardsSet.map((cardSet) =>
            cardSet.id === setId ? { ...cardSet, cards: deleteCard(cardSet.cards, cardId) } : cardSet,
          );
          set({ application: { ...application, cardsSet: updatedSets } });
        },
        markCardAsLearned: (setId) => {
          const { application } = get();
          const updatedSets = application.cardsSet.map((cardSet) =>
            cardSet.id === setId ? markCardAsLearned(cardSet) : cardSet,
          );
          const updatedCurrentSet = updatedSets.find((cardSet) => cardSet.id === setId);
          set({ application: { ...application, cardsSet: updatedSets }, currentSet: updatedCurrentSet });
        },
        moveCardToBottom: (setId) => {
          const { application } = get();
          const updatedSets = application.cardsSet.map((cardSet) =>
            cardSet.id === setId ? moveCardToBottom(cardSet) : cardSet,
          );
          const updatedCurrentSet = updatedSets.find((cardSet) => cardSet.id === setId);
          set({ application: { ...application, cardsSet: updatedSets }, currentSet: updatedCurrentSet });
        },
      },
    }),
    {
      name: "learning-words-app",
      storage: createJSONStorage(() => localStorage),
      partialize: (state) => ({ ...state, actions: undefined }),
    },
  ),
);
