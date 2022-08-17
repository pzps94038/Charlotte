export type Nav = {
  navigate: (
    value: string,
    option?: {
      screen: string;
    }
  ) => void;
};
