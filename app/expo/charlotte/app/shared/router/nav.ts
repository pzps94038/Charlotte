export type Nav = {
  navigate: (
    value: string,
    option?:
      | {
          screen: string;
        }
      | {
          [key: string]: any;
        }
  ) => void;
  dispatch: any;
  setOptions: (params: { title?: string } | { [key: string]: any }) => void;
};
