
function [list,data] =  runLenghtEncoding(flt, level)
    [nf1,nf2]=size(flt);  % Indexfeld
   
    list = zeros(nf1,8);
    data=list;
    eps =0.05;
     for i1=1:nf1
           n=1;
           isOut=flt(i1,1)<(level-eps); 
           for i2=1:nf2
              
               if isOut 
                   if  flt(i1,i2)>=(level+eps)
                     isOut=false;
                     list(i1,n)= i2;
                     data(i1,n)= 1;
                     n=n+1;
                   end
               else 
                   if  flt(i1,i2)<(level-eps)
                     isOut=true;
                     list(i1,n)= i2;
                     data(i1,n)=-1;
                     n=n+1;
                   end
                end
           end
     end