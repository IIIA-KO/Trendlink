import React, { useState } from 'react';
import { CooperationType } from "../types/CooperationType";

interface EditCooperationModalProps {
    cooperation: CooperationType | null;
    onClose: () => void;
    onSave: (updatedCooperation: CooperationType) => void;
}

const EditCooperationModal: React.FC<EditCooperationModalProps> = ({ cooperation, onClose, onSave }) => {
    const [updatedCooperation, setUpdatedCooperation] = useState<CooperationType | null>(cooperation);

    if (!updatedCooperation) {
        return null;
    }

    return (
        <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center">
            <div className="bg-white p-6 rounded-lg shadow-lg w-1/3">
                <h3 className="text-lg font-semibold mb-4">Редагувати кооперацію</h3>
                <form
                    onSubmit={(e) => {
                        e.preventDefault();
                        onSave(updatedCooperation);
                    }}
                >
                    <div className="mb-4">
                        <label className="block text-sm font-semibold mb-2" htmlFor="cooperationName">
                            Назва кооперації
                        </label>
                        <input
                            type="text"
                            id="cooperationName"
                            value={updatedCooperation.name}
                            onChange={(e) => setUpdatedCooperation({ ...updatedCooperation, name: e.target.value })}
                            className="border border-gray-300 p-2 rounded w-full"
                        />
                    </div>

                    <div className="mb-4">
                        <label className="block text-sm font-semibold mb-2" htmlFor="date">
                            Дата
                        </label>
                        <input
                            type="date"
                            id="date"
                            value={new Date(updatedCooperation.scheduledOnUtc).toISOString().split('T')[0]}
                            onChange={(e) =>
                                setUpdatedCooperation({ ...updatedCooperation, scheduledOnUtc: new Date(e.target.value).toISOString() })
                            }
                            className="border border-gray-300 p-2 rounded w-full"
                        />
                    </div>

                    <div className="mb-4">
                        <label className="block text-sm font-semibold mb-2" htmlFor="time">
                            Час
                        </label>
                        <input
                            type="time"
                            id="time"
                            value={new Date(updatedCooperation.scheduledOnUtc).toLocaleTimeString('uk-UA', {
                                hour: '2-digit',
                                minute: '2-digit',
                            })}
                            onChange={(e) => {
                                const [hours, minutes] = e.target.value.split(':');
                                const newDate = new Date(updatedCooperation.scheduledOnUtc);
                                newDate.setHours(parseInt(hours));
                                newDate.setMinutes(parseInt(minutes));
                                setUpdatedCooperation({ ...updatedCooperation, scheduledOnUtc: newDate.toISOString() });
                            }}
                            className="border border-gray-300 p-2 rounded w-full"
                        />
                    </div>

                    <div className="mb-4">
                        <label className="block text-sm font-semibold mb-2" htmlFor="description">
                            Опис
                        </label>
                        <textarea
                            id="description"
                            value={updatedCooperation.description}
                            onChange={(e) => setUpdatedCooperation({ ...updatedCooperation, description: e.target.value })}
                            className="border border-gray-300 p-2 rounded w-full"
                        />
                    </div>

                    <div className="flex justify-end space-x-2">
                        <button
                            type="button"
                            className="px-4 py-2 bg-red-500 text-white rounded-md hover:bg-red-600"
                            onClick={onClose}
                        >
                            Закрити
                        </button>
                        <button
                            type="submit"
                            className="px-4 py-2 bg-green-500 text-white rounded-md hover:bg-green-600"
                        >
                            Зберегти зміни
                        </button>
                    </div>
                </form>
            </div>
        </div>
    );
};

export default EditCooperationModal;