using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using static CitizenFX.Core.Native.API;

namespace slt_weirdcrash.Client
{
    public class ClientMain : BaseScript
    {
		static int iLocal_128 = 0;
		static int iLocal_129 = 0;
		static float fLocal_126 = 0f;
		static float fLocal_127 = 0f;
		GameColor f_107_17 = new GameColor(255, 255, 255, 255);
		static Dictionary<Tuple<int, int>, int> local_132_31 = new Dictionary<Tuple<int, int>, int>();
        static bool setup_completed = false;

        public ClientMain()
        {
			RequestStreamedTextureDict("mparcadecabinetgridtiles", true);
			generate_random_tilelayout();
		}

        [Tick]
        public Task OnTick()
        {
			if (setup_completed)
			{
				int iVar0 = 0;
				while (iVar0 <= 5)
				{
					int iVar1 = 0;
					while (iVar1 <= 3)
					{
						string Var2 = "top_down_tile_";
						int iVar3 = local_132_31[new Tuple<int, int>(iVar0, iVar1)] + 1;
						Var2 += iVar3;
						Tuple<float, float> Var4 = calculate_tile_location(iVar0, iVar1);
						draw_tile_on_screen("MPArcadeCabinetGridTiles", Var2, Var4.Item1, Var4.Item2, 0.104167f, 0.185185f, 0f, f_107_17);
						iVar1++;
					}
					iVar0++;
				}
			}

			return Task.FromResult(0);
        }

		Tuple<float, float> calculate_tile_location(int iParam0, int iParam1)
		{
			Tuple<float, float> Var0;

			float item_1 = (0.1380207f + (0.1041665f * (float)(iParam0 + 1)));
			float item_2 = (0.06851837f + (0.1851855f * (float)(iParam1 + 1)));

			Var0 = new Tuple<float, float>(item_1, item_2);
			return Var0;
		}

		void draw_tile_on_screen(string sParam0, string sParam1, float fParam2, float fParam3, float fParam4, float fParam5, float fParam6, GameColor Param7)
		{
			int iVar0;

			GetActiveScreenResolution(ref iLocal_128, ref iLocal_129);
			fLocal_126 = GetAspectRatio(false);
			if (IsPcVersion())
			{
				if (fLocal_126 >= 4f)
				{
					fLocal_126 = (fLocal_126 / 3f);
				}
			}

			fLocal_127 = (1.778f / fLocal_126);

			if (IsStringNullOrEmpty(sParam1))
			{
				return;
			}
			if (IsStringNullOrEmpty(sParam0))
			{
				return;
			}
			iVar0 = Round((fParam3 * (float)(iLocal_129)));
			fParam3 = (ToFloat(iVar0) * (1f / (float)(iLocal_129)));
			iVar0 = Round(((fParam5 * (float)(iLocal_129)) / 4f)) * 4;
			fParam5 = (ToFloat(iVar0) * (1f / (float)(iLocal_129)));
			Function.Call((Hash)0x2D3B147AFAD49DE0, sParam0, sParam1, calculate_drawing_coords(fParam2), fParam3, (fParam4 * fLocal_127), fParam5, fParam6, Param7.R, Param7.G, Param7.B, Param7.A, 0, 1);
		}

		float calculate_drawing_coords(float fParam0)
		{
			fParam0 = (((fParam0 * 1920f) - ((1920f - 1080f) / 2f)) / 1080f);
			fParam0 = (0.5f - ((0.5f - fParam0) / fLocal_126));
			return fParam0;
		}

		static void generate_random_tilelayout()
		{
			int iVar0;
			int iVar1;
			int[] iVar2 = new int[] { };
			int iVar3;
			int iVar4;

			iVar0 = 0;
			while (iVar0 <= 23)
			{
				iVar2[iVar0] = iVar0;
				iVar0++;
			}
			SetRandomSeed(GetNetworkTime());
			iVar0 = 0;
			while (iVar0 <= 23)
			{
				iVar1 = GetRandomIntInRange(0, 24);
				iVar3 = iVar2[iVar0];
				iVar2[iVar0] = iVar2[iVar1];
				iVar2[iVar1] = iVar3;
				iVar0++;
			}
			iVar4 = 0;
			iVar0 = 0;
			while (iVar0 <= 5)
			{
				iVar1 = 0;
				while (iVar1 <= 3)
				{
					local_132_31.Add(new Tuple<int, int>(iVar0, iVar1), iVar4);
					iVar4++;
					iVar1++;
				}
				iVar0++;
			}

			setup_completed = true;
		}
	}

	struct GameColor
	{
		public int R;
		public int G;
		public int B;
		public int A;

		public GameColor(int r, int g, int b, int a)
		{
			R = r;
			G = g;
			B = b;
			A = a;
		}
	}
}