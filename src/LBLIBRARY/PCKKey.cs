namespace LBLIBRARY
{
    using System;

    public class PCKKey
    {
        public int KEY_1;
        public int KEY_2;
        public int ASIG_1;
        public int ASIG_2;
        public int FSIG_1;
        public int FSIG_2;

        public PCKKey()
        {
            this.KEY_1 = -1466731422;
            this.KEY_2 = -240896429;
            this.ASIG_1 = -33685778;
            this.ASIG_2 = -267534609;
            this.FSIG_1 = 0x4dca23ef;
            this.FSIG_2 = 0x56a089b7;
        }

        public PCKKey(int key1, int key2)
        {
            this.KEY_1 = -1466731422;
            this.KEY_2 = -240896429;
            this.ASIG_1 = -33685778;
            this.ASIG_2 = -267534609;
            this.FSIG_1 = 0x4dca23ef;
            this.FSIG_2 = 0x56a089b7;
            this.KEY_1 = key1;
            this.KEY_2 = key2;
        }

        public PCKKey(int key1, int key2, int asig1, int asig2, int fsig1, int fsig2)
        {
            this.KEY_1 = -1466731422;
            this.KEY_2 = -240896429;
            this.ASIG_1 = -33685778;
            this.ASIG_2 = -267534609;
            this.FSIG_1 = 0x4dca23ef;
            this.FSIG_2 = 0x56a089b7;
            this.KEY_1 = key1;
            this.KEY_2 = key2;
            this.ASIG_1 = asig1;
            this.ASIG_2 = asig2;
            this.FSIG_1 = fsig1;
            this.FSIG_2 = fsig2;
        }
    }
}

