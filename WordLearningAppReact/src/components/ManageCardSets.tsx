import { useState } from "react";
import { CardSet } from "../types/CardSet";
import { AddNewSet, DeleteAppSet } from "../types/Application";

type ManageCardSetsProps = {
  application: { cardsSet: CardSet[] };
  setApplication: (app: { cardsSet: CardSet[] }) => void;
  handleSelectSet: (set: CardSet) => void;
};

export const ManageCardSets = ({ application, setApplication, handleSelectSet }: ManageCardSetsProps) => {
  const [newSetName, setNewSetName] = useState("");

  const handleAddSet = () => {
    if (newSetName.trim() === "") return;
    const updatedApp = AddNewSet(application, newSetName);
    setApplication(updatedApp);
    setNewSetName("");
  };

  const handleDeleteSet = (id: string) => {
    const updatedApp = DeleteAppSet(application, id);
    setApplication(updatedApp);
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
