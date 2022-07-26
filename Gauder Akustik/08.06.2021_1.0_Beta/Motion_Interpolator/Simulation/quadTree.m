function node=quadTree(flt,lev,maxLevel)
%Quadtree Wandelt das Kollisionsfeld in einen quaddtree
% flt Feld 
% lev ebene bis zu der aufgelöst wird bei 256 solte es 8 sein
  
   [nf1,nf2]=size(flt);  % Indexfelt
   % nf1=512; nf2=512;
 
  % node = {0 0 0 0};
   k1=int16(nf1/2); k2=int16(nf2/2);
   kn= [0 0; k1 0; 0 k2; k1 k1];
   
   for ind= 1:4
       n=  sum(flt(kn(ind,1)+(1:k1) ,kn(ind,2)+(1:k2))<0.3,'all');  
       if n>0 
       if n== k1*k2 
           node(ind)={k1}; 
       else
       if lev >= maxLevel
         node(ind) = {55};  
       else
         node{ind}= quadTree(flt(kn(ind,1)+(1:k1) ,kn(ind,2)+(1:k2) ),lev+1,maxLevel ) ;
       end
       end
       else
         node(ind)={77};
         end
   end  
   
%    n1=  sum(flt(1:k1 ,1:k2 )<0.3,'all');
%    if n1>0 
%        if n1== k1*k2 
%            node.q1 = k1*k2; 
%            node.q(1)=k1*k2; 
%        else
%        if lev >= maxLev
%          node.q1 = 1;  
%        else
%        node.q1= quadTree(flt(1:k1 ,1:k2 ),lev+1) 
%        end
%        end
%         else
%      node.q1=0;
%    end
%   
%    n2=  sum(flt(k1+1:end,1:k2 )<0.3,'all');
%      if n2>0 
%                 if n2== k1*k2 
%            node.q2 = k1*k2; 
%        else
%  
%           if lev >= maxLev
%          node.q2 = 1;  
%        else
%        node.q2= quadTree(flt(k1+1:end,1:k2),lev+1) 
%           end
%                 end
%        else
%          node.q2=0;
%    end
%    n3=  sum(flt(1:k1 ,k2+1:end)<0.3,'all');
%      if n3>0  
%                 if n3== k1*k2 
%            node.q3 = k1*k2; 
%        else
%  
%           if lev >= maxLev
%          node.q3 = 1;  
%        else
%         node.q3= quadTree(flt(1:k1 ,k2+1:end),lev+1 )
%           end
%                 end
%           else
%          node.q3=0;
%    end
%      
%    n4=  sum(flt(k1+1:end,k2+1:nf2)<0.3,'all');
%      if n4>0  
%                 if n4== k1*k2 
%            node.q4 = k1*k2; 
%        else
%  
%          if lev >= maxLev
%          node.q4 = 1;  
%        else
%         node.q4= quadTree(flt(k1+1:end,k2+1:end),lev+1 )
%          end
%                 end
%       else
%          node.q4=0;
%    end
 end


