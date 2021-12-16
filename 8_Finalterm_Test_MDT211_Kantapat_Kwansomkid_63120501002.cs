using System;

namespace _8_Finalterm_Test_MDT211_Kantapat_Kwansomkid_63120501002
{
    class Program
    {
        static Queue FactoryProduct(ref char[] FactoryProduct, ref char[,] FactoryProduct, ref Queue queue)
        {
            Queue exceedQueue = new Queue();

            while (queue.GetLength() > 0)
            {
                Node node = queue.Pop();

                int FactoryProduct = Array.IndexOf(FactoryProductInstruction, node.Instruction);

                if (FactoryProductWithInstructionIndex == -1)
                {
                    int FactoryProductEmptyInstructionIndex = Array.IndexOf(FactoryProductInstruction, '\0');

                    if (FactoryProductEmptyInstructionIndex == -1)
                    {
                        queue.Insert(node, 0);
                        break;
                    }
                    else
                    {
                        FactoryProductInstruction[FactoryProductEmptyInstructionIndex] = node.Instruction;
                        FactoryProductData[FactoryProductEmptyInstructionIndex, 0] = node.Data;
                    }
                }
                else
                {
                    bool canProcess = false;
                    for (int i = 0; i < FactoryProductData.GetLength(1); i++)
                    {
                        if (FactoryProductData[FactoryProductWithInstructionIndex, i] == node.Data)
                        {
                            canProcess = true;
                            break;
                        }
                        else if (FactoryProductData[FactoryProductWithInstructionIndex, i] == '\0')
                        {
                            FactoryProductData[FactoryProductWithInstructionIndex, i] = node.Data;
                            canProcess = true;
                            break;
                        }
                    }

                    if (!canProcess)
                    {
                        exceedQueue.Push(node);
                    }
                }

            }

            return exceedQueue;
        }

        static int ProcessInstructionAndData(ref char[] FactoryProductInstruction, ref char[,] FactoryProductData, ref Queue mainQueue, ref Queue temporaryQueue)
        {
            int cycle = 0;

            while (temporaryQueue.GetLength() + mainQueue.GetLength() > 0)
            {
                cycle++;

                if (temporaryQueue.GetLength() > 0)
                {
                    Queue exceedQueue = AllocateInstructionAndDataToCpu(ref FactoryProductInstruction, ref FactoryProductData, ref temporaryQueue);
                    temporaryQueue.Join(exceedQueue);
                }

                if (mainQueue.GetLength() > 0)
                {
                    Queue exceedQueue = AllocateInstructionAndDataToFactoryProduct(ref FactoryProductInstruction, ref FactoryProductData, ref mainQueue);
                    temporaryQueue.Join(exceedQueue);
                }

                FactoryProductInstruction = new char[FactoryProductInstruction.Length];
                FactoryProductData = new char[FactoryProductData.GetLength(0), FactoryProductData.GetLength(1)];
            }

            return cycle;
        }

        static void Main(string[] args)
        {
            char[] FactoryProductInstruction = new char[4];
            char[,] FactoryProductData = new char[4, 3];

            Queue mainQueue = new Queue();
            Queue temporaryQueue = new Queue();

            char inputInstruction, inputData;
            while (true)
            {
                inputInstruction = char.Parse(Console.ReadLine());

                if (inputInstruction == '?')
                {
                    break;
                }

                inputData = char.Parse(Console.ReadLine());

                Node inputNode = new Node(inputInstruction, inputData);
                mainQueue.Push(inputNode);
            }

            int Parts = ProcessInstructionAndData(ref FactoryProductInstruction, ref FactoryProductData, ref mainQueue, ref temporaryQueue);
            Console.WriteLine("{0}", Parts);
        }
    }
}
