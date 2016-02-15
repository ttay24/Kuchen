﻿using System.Linq;
using System.Collections.Generic;

namespace Kuchen.Util
{
	public static class PatternMatch
	{
		public static bool IsMatch(string mask, string full)
		{
			if(mask == full) return true;
			
			IEnumerable<char> input = full.AsEnumerable();
			for(int i = 0; i < mask.Length; i++)
			{
				switch(mask[i])
				{
					case '?':
						if(!input.Any()) return false;
						input = input.Skip(1);
						break;
					
					case '*':
						if(mask.Length == i + 1) return true;
						while(input.Any() && input.First() != mask[i+1])
						{
							input = input.Skip(1);
						}
						break;
					
					default:
						if(!input.Any() || input.First() != mask[i]) return false;
						input = input.Skip(1);
						break;
				}
			}
			return !input.Any();
		}
	}	
}