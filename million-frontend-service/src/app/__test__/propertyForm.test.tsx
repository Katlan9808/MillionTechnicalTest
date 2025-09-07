import { render, screen, fireEvent, waitFor } from '@testing-library/react';
import PropertyForm from '@/app/components/propertyForm';
import api from '@/lib/api';
import userEvent from '@testing-library/user-event';
import { PropertyDTO } from '@/types/property';
import { describe, beforeEach } from 'node:test';
import { it } from 'zod/locales';

jest.mock('@/lib/api');

const mockOnSuccess = jest.fn();

describe('PropertyForm', () => {
  beforeEach(() => {
    jest.clearAllMocks();
  });

  it('renders empty form by default', () => {
    render(<PropertyForm onSuccess={mockOnSuccess} />);
    expect(screen.getByPlaceholderText('Name')).toHaveValue('');
    expect(screen.getByPlaceholderText('Price')).toHaveValue(0);
  });

  it('shows validation errors on empty submit', async () => {
    render(<PropertyForm onSuccess={mockOnSuccess} />);
    fireEvent.click(screen.getByRole('button', { name: /Create Property/i }));

    await waitFor(() => {
      expect(screen.getByText(/Required/i)).toBeInTheDocument();
    });
  });

  it('submits form and calls POST when creating', async () => {
    (api.post as jest.Mock).mockResolvedValue({});

    render(<PropertyForm onSuccess={mockOnSuccess} />);

    userEvent.type(screen.getByPlaceholderText('Name'), 'Casa Bonita');
    userEvent.type(screen.getByPlaceholderText('Address'), 'Calle 123');
    userEvent.type(screen.getByPlaceholderText('Price'), '500000');
    userEvent.type(screen.getByPlaceholderText('Code Internal'), 'CB001');
    userEvent.type(screen.getByPlaceholderText('Year'), '2023');
    userEvent.type(screen.getByPlaceholderText('Owner ID'), 'OWN001');

    fireEvent.click(screen.getByRole('button', { name: /Create Property/i }));

    await waitFor(() => {
      expect(api.post).toHaveBeenCalledWith(
        expect.stringContaining('/property'),
        expect.objectContaining({
          name: 'Casa Bonita',
          price: '500000',
          idOwner: 'OWN001',
        })
      );
      expect(mockOnSuccess).toHaveBeenCalled();
    });
  });

  it('submits form and calls PUT when updating', async () => {
    (api.put as jest.Mock).mockResolvedValue({});

    const initialData: PropertyDTO = {
      idProperty: 'PROP001',
      name: 'Casa Bonita',
      address: 'Calle 123',
      price: 500000,
      codeInternal: 'CB001',
      year: 2023,
      idOwner: 'OWN001',
    };

    render(<PropertyForm initialData={initialData} onSuccess={mockOnSuccess} />);

    fireEvent.click(screen.getByRole('button', { name: /Update Property/i }));

    await waitFor(() => {
      expect(api.put).toHaveBeenCalledWith(
        '/property/PROP001',
        expect.objectContaining({
          idProperty: 'PROP001',
          name: 'Casa Bonita',
        })
      );
      expect(mockOnSuccess).toHaveBeenCalled();
    });
  });
});