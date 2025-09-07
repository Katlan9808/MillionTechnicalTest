'use client';
import { useEffect, useState } from 'react';
import api from '@/lib/api';
import { PropertyDTO } from '@/types/property';
import PropertyForm from '@/app/components/propertyForm';

export default function PropertiesPage() {
  const [properties, setProperties] = useState<PropertyDTO[]>([]);
  const [loading, setLoading] = useState(true);
  const [selectedProperty, setSelectedProperty] = useState<PropertyDTO | undefined>(undefined);
  

  useEffect(() => {
    api.get('/property')
      .then(res => setProperties(res.data.result))
      .catch(err => console.error(err))
      .finally(() => setLoading(false));
  }, []);

  return (
    <div className="p-6">
      <PropertyForm
        initialData={selectedProperty}
        onSuccess={() => {
          setSelectedProperty(undefined);
          api.get('/property')
            .then(res => setProperties(res.data.result))
            .catch(err => console.error(err));
        }}
      />

      <h1 className="text-2xl font-bold mb-4">Properties</h1>
      {loading ? (
        <p>Loading...</p>
      ) : (
        <ul className="space-y-4">
         {properties.map(prop => (
            <li key={prop.idProperty} className="border p-4 rounded shadow">
              <h2 className="text-xl font-semibold">{prop.name}</h2>
              <p>Address: {prop.address}</p>
              <p>Price: ${prop.price}</p>
              <p>Code: {prop.codeInternal}</p>
              <div className="mt-2 flex gap-2">
                <button
                  className="px-3 py-1 bg-blue-500 text-white rounded"
                  onClick={() => setSelectedProperty(prop)}
                >
                  Edit
                </button>
                <button
                  className="px-3 py-1 bg-red-500 text-white rounded"
                  onClick={async () => {
                    if (confirm('Are you sure you want to delete this property?')) {
                      try {
                        await api.delete(`/property/${prop.idProperty}`);
                        setProperties(prev => prev.filter(p => p.idProperty !== prop.idProperty));
                      } catch (err) {
                        console.error(err);
                        alert('Failed to delete property');
                      }
                    }
                  }}
                >
                  Delete
                </button>
              </div>
            </li>
          ))}
        </ul>
      )}
    </div>
  );
}