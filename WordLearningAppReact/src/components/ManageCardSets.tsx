import { useState } from "react";
import { CardSet } from "../types/CardSet";
import { useStore } from "../hooks/useStore";

type ManageCardSetsProps = {
  handleSelectSet: (set: CardSet) => void;
};

export const ManageCardSets = ({ handleSelectSet }: ManageCardSetsProps) => {
  const [newSetName, setNewSetName] = useState("");
  const { application, actions } = useStore();

  const handleAddSet = () => {
    if (newSetName.trim() === "") return;
    actions.addCardSet(newSetName);
    setNewSetName("");
  };

  const handleDeleteSet = (id: string) => {
    actions.deleteCardSet(id);
  };

  return (
    <div>
      <h1>Card Sets Management</h1>
      <div>
        <input
          type="text"
          placeholder="Enter new set name"
          value={newSetName}
          onChange={(e) => {
            setNewSetName(e.target.value);
          }}
        />
        <button onClick={handleAddSet}>Add Set</button>
      </div>
      <div>
        {application.cardsSet.length === 0 ? (
          <p>No card sets available.</p>
        ) : (
          application.cardsSet.map((set) => (
            <div key={set.id}>
              <h2>{set.name}</h2>
              <button
                onClick={() => {
                  handleSelectSet(set);
                }}
              >
                Start Learning Process
              </button>
              <button
                onClick={() => {
                  handleDeleteSet(set.id);
                }}
              >
                Delete Set
              </button>
            </div>
          ))
        )}
      </div>
    </div>
  );
};
