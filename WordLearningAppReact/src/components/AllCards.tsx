import { useState } from "react";
import { CardSet } from "../types/CardSet";
import { CreateCard, UpdateCard, DeleteCard } from "../types/Card";

type AllCardsProps = {
  currentSet: CardSet;
  setCurrentSet: (set: CardSet | null) => void;
  application: { cardsSet: CardSet[] };
  setApplication: (app: { cardsSet: CardSet[] }) => void;
  handleBackToLearning: () => void;
};

export const AllCards = ({
  currentSet,
  setCurrentSet,
  application,
  setApplication,
  handleBackToLearning,
}: AllCardsProps) => {
  const [newWord, setNewWord] = useState("");
  const [newTranslation, setNewTranslation] = useState("");

  const handleCreateCard = () => {
    if (newWord.trim() && newTranslation.trim()) {
      const updatedSet = CreateCard(newWord, newTranslation, currentSet);
      const updatedApp = {
        ...application,
        cardsSet: [...application.cardsSet, updatedSet],
      };
      setApplication(updatedApp);
      setCurrentSet(updatedSet);
      setNewWord("");
      setNewTranslation("");
    }
  };

  const handleUpdateCard = (id: string, updatedWord: string, updatedTranslation: string) => {
    const updatedSet = UpdateCard(currentSet, id, updatedWord, updatedTranslation);
    const updatedApp = {
      ...application,
      cardsSet: [...application.cardsSet, updatedSet],
    };
    setApplication(updatedApp);
    setCurrentSet(updatedSet);
  };

  const handleDeleteCard = (id: string) => {
    const updatedSet = DeleteCard(currentSet, id);
    const updatedApp = {
      ...application,
      cardsSet: [...application.cardsSet, updatedSet],
    };
    setApplication(updatedApp);
    setCurrentSet(updatedSet);
  };

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
                  handleUpdateCard(
                    card.id,
                    prompt("New Word:", card.word) ?? card.word,
                    prompt("New Translation:", card.translation) ?? card.translation,
                  );
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
