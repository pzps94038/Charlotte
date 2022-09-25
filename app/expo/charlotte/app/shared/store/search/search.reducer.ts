import {createSlice, PayloadAction} from '@reduxjs/toolkit';

const searchSlice = createSlice({
  name: 'search',
  initialState: '',
  reducers: {
    changeSearch: (state, action: PayloadAction<string>) => {
      return action.payload;
    },
    clearSearch: () => {
      return '';
    },
  },
});

export default searchSlice.reducer;
export const {changeSearch, clearSearch} = searchSlice.actions;
