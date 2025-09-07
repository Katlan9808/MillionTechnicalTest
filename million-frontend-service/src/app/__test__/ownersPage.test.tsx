import { render, screen, waitFor } from '@testing-library/react';
import OwnersPage from '@/app/owners/page';
import api from '@/lib/api';
import { OwnerDTO } from '@/types/owner';
import { describe, beforeEach } from 'node:test';
import { it } from 'zod/locales';

jest.mock('@/lib/api');

const mockOwners: OwnerDTO[] = [
  {
    idOwner: 'OWN001',
    name: 'María López',
    address: 'Calle 28',
    photo: '',
    birthday: '1990-05-12',
  },
];

describe('OwnersPage', () => {
  beforeEach(() => {
    (api.get as jest.Mock).mockResolvedValue({
      data: { result: mockOwners },
    });
  });

  it('renders loading state initially', () => {
    render(<OwnersPage />);
    expect(screen.getByText(/Loading.../i)).toBeInTheDocument();
  });

  it('renders owners after fetch', async () => {
    render(<OwnersPage />);
    await waitFor(() => {
      expect(screen.getByText('María López')).toBeInTheDocument();
      expect(screen.getByText('Calle 28')).toBeInTheDocument();
      expect(screen.getByText(/Birthday:/)).toBeInTheDocument();
    });
  });
});