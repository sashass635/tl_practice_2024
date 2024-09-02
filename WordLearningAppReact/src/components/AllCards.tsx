import { useState } from "react";
import { useStore } from "../hooks/useStore";

type AllCardsProps = {
  handleBackToLearning: () => void;
};

export const AllCards = ({ handleBackToLearning }: AllCardsProps) => {
  const { currentSet, actions } = useStore((state) => ({
    currentSet: state.currentSet,
    actions: state.actions,
  }));

  const [newWord, setNewWord] = useState("");
  const [newTranslation, setNewTranslation] = useState("");

  const handleCreateCard = () => {
    if (currentSet && newWord.trim() && newTranslation.trim()) {
      actions.addCardToSet(currentSet.id, newWord, newTranslation);
      setNewWord("");
      setNewTranslation("");
      actions.setCurrentSet(currentSet);
    }
  };

  const handleUpdateCard = (id: string, updatedWord: string, updatedTranslation: string) => {
    if (currentSet) {
      actions.updateCardInSet(currentSet.id, id, updatedWord, updatedTranslation);
      actions.setCurrentSet(currentSet);
    }
  };

  const handleDeleteCard = (id: string) => {
    if (currentSet) {
      actions.deleteCardFromSet(currentSet.id, id);
      actions.setCurrentSet(currentSet);
    }
  };

  if (!currentSet) {
    return <div>No set selected.</div>;
  }

  return (
    <div>
      <h1>{currentSet.name} - All Cards</h1>
      <div>
        <input
          type="text"
          placeholder="New Word"
          value={newWord}
          onChange={(e) => {
            setNewWord(e.target.value);
          }}
        />
        <input
          type="text"
          placeholder="Translation"
          value={newTranslation}
          onChange={(e) => {
            setNewTranslation(e.target.value);
          }}
        />
        <button onClick={handleCreateCard}>Add Card</button>
      </div>
      <div>
        {currentSet.cards.length === 0 ? (
          <p>No cards in this set.</p>
        ) : (
          currentSet.cards.map((card) => (
            <div key={card.id}>
              <p>
                <strong>Word:</strong> {card.word}
              </p>
              <p>
                <strong>Translation:</strong> {card.translation}
              </p>
              <button
                onClick={() => {
                  const updatedWord = prompt("New Word:", card.word) ?? card.word;
                  const updatedTranslation = prompt("New Translation:", card.translation) ?? card.translation;
                  handleUpdateCard(card.id, updatedWord, updatedTranslation);
                }}
              >
                Edit
              </button>
              <button
                onClick={() => {
                  handleDeleteCard(card.id);
                }}
              >
                Delete
              </button>
            </div>
          ))
        )}
      </div>
      <footer>
        <button onClick={handleBackToLearning}>Back to Learning</button>
      </footer>
    </div>
  );
};
